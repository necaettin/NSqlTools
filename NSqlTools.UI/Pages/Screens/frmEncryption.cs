using BOA.Common.Helpers;
using BOA.Common.Types;
using DiffPlex;
using NSqlTools.Types;
using NSqlTools.Types.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSqlTools.UI.Pages.Screens
{
	public partial class frmEncryption : Form
	{
		#region Constructors
		public frmEncryption()
		{
			InitializeComponent();

			initForm();
		}
		#endregion

		#region Properties \ Enums
		private enum EncryptionAlgorithm
		{
			Rijindael = 1,
			Sha256 = 2,
			SData = 3,
			AES = 4,
			Journal = 5
		}

		private EncryptionAlgorithm SelectedEncryptionAlgorithm 
		{ 
			get
			{
				EncryptionAlgorithm ea = EncryptionAlgorithm.Rijindael;
				if(rbRijndael.Checked)
					ea = EncryptionAlgorithm.Rijindael;
				else if (rbSha256.Checked)
					ea = EncryptionAlgorithm.Sha256;
				else if (rbSData.Checked)
					ea = EncryptionAlgorithm.SData;
				else if (rbAES.Checked)
					ea = EncryptionAlgorithm.AES;
				else if (rbJournal.Checked)
					ea = EncryptionAlgorithm.Journal;

				return ea;
			}
		}
		#endregion

		#region Events
		private void tsbRun_Click(object sender, EventArgs e)
		{
			if (!validateFields())
				return;

			encryptDecrypt();
		}

		private void encryptionAlgorithm_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton checkBox = sender as RadioButton;
			EncryptionAlgorithm ea = (EncryptionAlgorithm)Convert.ToInt32(checkBox.Tag);

			rbEncrypt.Enabled = true;
			rbEncrypt.Checked = true;
			rbDecrypt.Enabled = true;
			txtKey.Text = null;
			txtOutput.Text = null;

			switch (ea)
			{
				case EncryptionAlgorithm.Rijindael:
					txtKey.Text = "BesiktasSenBizimHerseyimizsin";

					break;
				case EncryptionAlgorithm.Sha256:
					rbDecrypt.Enabled = false;
					rbEncrypt.Checked = true;

					break;
				case EncryptionAlgorithm.SData:
					break;
				case EncryptionAlgorithm.AES:
					break;
				case EncryptionAlgorithm.Journal:
					rbEncrypt.Enabled = false;
					rbDecrypt.Checked = true;

					break;
				default:
					break;
			}
		}
		#endregion

		#region Methods
		private void initForm()
		{
			encryptionAlgorithm_CheckedChanged(rbRijndael, EventArgs.Empty);

			setTextFromResource();
		}

		private void encryptDecrypt()
		{
			String key = txtKey.Text;
			String input = txtInput.Text;
			String output = String.Empty;
			try
			{
				switch (SelectedEncryptionAlgorithm)
				{
					case EncryptionAlgorithm.Rijindael:
						output = rbEncrypt.Checked
							? BOA.Common.Helpers.EncryptionHelper.EncryptByRijndael(input, key)
							: BOA.Common.Helpers.EncryptionHelper.DecryptByRijndael(input, key);

						break;
					case EncryptionAlgorithm.Sha256:
						output = rbEncrypt.Checked
							? BOA.Common.Helpers.EncryptionHelper.Sha256Encrypt(input)
							: null;

						break;
					case EncryptionAlgorithm.SData:
						output = rbEncrypt.Checked
							? EncryptSecureData(input)
							: DecryptSecureData(input);

						break;
					case EncryptionAlgorithm.AES:
						AES aes = new AES(key);
						output = rbEncrypt.Checked
							? aes.Encrypt(input)
							: aes.Decrypt(input);

						break;
					case EncryptionAlgorithm.Journal:
						if (rbDecrypt.Checked)
						{
							JournalDecompress jd = new JournalDecompress();
							var hexBytes = jd.HexToBytes(input);
							byte[] decompressedBytes = jd.DecompressBuffer(hexBytes);
							var response = BOA.Common.Helpers.SerializeHelper.ByteToObject(decompressedBytes);
						}

						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, String.Format(CommonResource.ErrorOccuredErrorDetail, ex.Message), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			txtOutput.Text = output;
		}

		private bool validateFields()
		{
			bool isValid = true;

			if ((SelectedEncryptionAlgorithm == EncryptionAlgorithm.Rijindael
				|| SelectedEncryptionAlgorithm == EncryptionAlgorithm.AES)
				&& !UIHelper.ComponentIsValidString(errorProvider, txtKey.Text, txtKey, NSqlTools.Types.Properties.CommonResource.FillKeyValue))
				isValid = false;

			if (!UIHelper.ComponentIsValidString(errorProvider, txtInput.Text, txtInput, NSqlTools.Types.Properties.CommonResource.FillInputValue))
				isValid = false;

			return isValid;
		}

		private void setTextFromResource()
		{
			this.gbEncryptDecrypt.Text = NSqlTools.Types.Properties.CommonResource.EncryptDecrypt;
			this.rbEncrypt.Text = NSqlTools.Types.Properties.CommonResource.Encrypt;
			this.rbDecrypt.Text = NSqlTools.Types.Properties.CommonResource.Decrypt;
			this.gbMethod.Text = NSqlTools.Types.Properties.CommonResource.Method;
			this.rbSha256.Text = NSqlTools.Types.Properties.CommonResource.Sha256;
			this.rbRijndael.Text = NSqlTools.Types.Properties.CommonResource.Rijndael;
			this.rbSData.Text = NSqlTools.Types.Properties.CommonResource.SData;
			this.lblKey.Text = NSqlTools.Types.Properties.CommonResource.Key;
			this.gbInput.Text = NSqlTools.Types.Properties.CommonResource.Input;
			this.gbOutput.Text = NSqlTools.Types.Properties.CommonResource.Output;
			this.rbAES.Text = NSqlTools.Types.Properties.CommonResource.AES;
			this.tsbRun.Text = NSqlTools.Types.Properties.CommonResource.Run;
			this.rbJournal.Text = NSqlTools.Types.Properties.CommonResource.Journal;
			this.Text = NSqlTools.Types.Properties.CommonResource.BOAEncryptDecrypt;
		}
		#endregion

		#region Helper Methods

		private String EncryptSecureData(String data)
		{
			String encryptData = String.Empty;

			String privateKey = GetCertificatePrivateKey(); //DecryptSecureData metotu için privatekey değerine ulaşılır.
			if (String.IsNullOrEmpty(privateKey))
				return encryptData;
			try
			{
				encryptData = EncryptionHelper.EncryptSecureData(data, privateKey);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}


			return encryptData;
		}

		private String DecryptSecureData(String input)
		{
			String decryptedValue = BOA.Common.Helpers.EncryptionHelper.DecryptSecureDataKey(input, BOA.Common.Types.SecureDataCertificates.BOA_SData_General);

			if (String.IsNullOrEmpty(decryptedValue))
			{
				String sdataEncryptionKey = BOA.Common.Helpers.EncryptionHelper.DecryptSecureDataKey("60G8N5+jghh8OdMbz3dLeA==", BOA.Common.Types.SecureDataCertificates.BOA_SData_General);
				decryptedValue = "2.=>" + BOA.Common.Helpers.EncryptionHelper.DecryptSecureData(input, sdataEncryptionKey);
			}

			return decryptedValue;
		}

		private String GetCertificatePrivateKey()
		{
			//ServiceLogHelper.WriteLogExt("GetCertificatePrivateKey is running..");
			String privateKey = String.Empty;

			X509Certificate2 certificate = GetCertificate("BOA_SData_General");
			if (certificate == null)
			{

			}
			else
			{
				RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
				try
				{
					byte[] modulus = ((RSACryptoServiceProvider)certificate.PrivateKey).ExportParameters(false).Modulus;
					privateKey = Convert.ToBase64String(modulus);
					if (String.IsNullOrEmpty(privateKey))
					{

					}
				}
				catch (Exception ex)
				{

				}
			}

			return privateKey;
		}

		private X509Certificate2 GetCertificate(String name)
		{
			X509Certificate2 result;
			if (name == null)
			{
				result = null;
			}
			else
			{
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Enumerator enumerator = x509Store.Certificates.GetEnumerator();
				while (enumerator.MoveNext())
				{
					X509Certificate2 current = enumerator.Current;
					if (current.GetNameInfo(X509NameType.SimpleName, false).ToLower() == name.ToLower())
					{
						x509Store.Close();
						result = current;
						return result;
					}
				}
				x509Store.Close();
				result = null;
			}
			return result;
		}
		#endregion
	}

	public class AES
	{
		private byte[] key;
		public AES(string SecretKey)
		{
			string k = Pass2Key(SecretKey);
			Console.WriteLine("Key:       " + k);
			key = Encoding.UTF8.GetBytes(k);
		}

		public string Encrypt(string plainText)
		{
			if (plainText == null || plainText.Length <= 0)
			{
				throw new ArgumentNullException("plainText");
			}

			if (key == null || key.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}

			byte[] encrypted;
			using (var rijAlg = new RijndaelManaged())
			{
				rijAlg.BlockSize = 256;
				rijAlg.Key = key;
				rijAlg.Mode = CipherMode.CBC;
				rijAlg.Padding = PaddingMode.Zeros;
				rijAlg.IV = key;
				ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
				using (var msEncrypt = new MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}

						encrypted = msEncrypt.ToArray();
					}
				}
			}

			return System.Convert.ToBase64String(encrypted);
		}

		public string Decrypt(string encrypted)
		{
			byte[] cipherText = System.Convert.FromBase64String(encrypted);
			if (cipherText == null || cipherText.Length <= 0)
			{
				throw new ArgumentNullException("cipherText");
			}

			if (key == null || key.Length <= 0)
			{
				throw new ArgumentNullException("key");
			}

			string plaintext;
			using (var rijAlg = new RijndaelManaged())
			{
				rijAlg.BlockSize = 256;
				rijAlg.Key = key;
				rijAlg.Mode = CipherMode.CBC;
				rijAlg.Padding = PaddingMode.Zeros;
				rijAlg.IV = key;
				ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
				using (var msDecrypt = new MemoryStream(cipherText))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt))
						{
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plaintext.Replace("\0", "");
		}

		public string Pass2Key(string SecretKey)
		{
			return System.Convert.ToBase64String((new SHA512CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(SecretKey))).Substring(0, 32);
		}
	}

	public class JournalDecompress
	{
		public byte[] DecompressBuffer(byte[] buffer)
		{
			if (buffer == null || buffer.Count() == 0)
				return buffer;

			using (MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length))
			{
				using (MemoryStream decompressedStream = new MemoryStream())
				{
					int blockSize = 1024;
					byte[] tempBuffer = new byte[blockSize];
					using (GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						while (true)
						{
							int bytesRead = gzStream.Read(tempBuffer, 0, blockSize);
							if (bytesRead == 0)
								break;
							decompressedStream.Write(tempBuffer, 0, bytesRead);
						}
					}
					byte[] decompressedBytes = decompressedStream.ToArray();
					return decompressedBytes;
				}
			}

			return null;
		}

		public byte[] HexToBytes(string hex)
		{
			if (string.IsNullOrEmpty(hex))
				return new byte[0];

			if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				hex = hex.Substring(2);

			if (hex.Length % 2 != 0)
				throw new ArgumentException("Invalid hex string length.", nameof(hex));

			int len = hex.Length;
			var bytes = new byte[len / 2];

			for (int i = 0; i < len; i += 2)
			{
				int high = GetHexVal(hex[i]);
				int low = GetHexVal(hex[i + 1]);
				bytes[i / 2] = (byte)((high << 4) | low);
			}

			return bytes;
		}

		private static int GetHexVal(char c)
		{
			if (c >= '0' && c <= '9') return c - '0';
			if (c >= 'a' && c <= 'f') return c - 'a' + 10;
			if (c >= 'A' && c <= 'F') return c - 'A' + 10;
			throw new ArgumentException("Invalid hex character: " + c, nameof(c));
		}
	}
}
