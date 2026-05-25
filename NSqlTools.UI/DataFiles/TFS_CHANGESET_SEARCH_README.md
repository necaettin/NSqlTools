# TFS Changeset Arama Ekran?

Bu özellik, TFS (Team Foundation Server) üzerindeki changesetleri aramak ve test/main branchlerine merge durumunu kontrol etmek için tasarlanm??t?r.

## Özellikler

1. **Comment (Yorum) ile Arama**: Changeset yorumlar?nda anahtar kelime arayabilirsiniz
2. **Kullan?c? (Owner) Filtreleme**: Belirli bir kullan?c?n?n changesetlerini filtreleyebilirsiniz
3. **Tarih Aral???**: Ba?lang?ç ve biti? tarihi belirleyerek arama yapabilirsiniz
4. **Merge Durumu Kontrolü**: 
   - TEST branchine merge edilmemi? changesetleri gösterebilir
   - MAIN branchine merge edilmemi? changesetleri gösterebilirsiniz

## Kullan?m

1. **TFS URL**: TFS sunucu adresinizi girin (örn: http://your-tfs-server:8080/tfs/DefaultCollection)
2. **TFS Path**: Arama yapmak istedi?iniz klasör yolunu girin (örn: ProductAndDelivery/Destek)
   - Sistem bu klasörün alt?ndaki tüm firmalar? (BOA, DigitalBank, vb.) otomatik bulacakt?r
   - Her firman?n Dev, Test, Main branchlerini ke?fedecektir
3. **Yorum (Comment)**: Aramak istedi?iniz anahtar kelimeyi girin
4. **Kullan?c? (Owner)**: "Kullan?c?lar? Yükle" butonuna t?klayarak kullan?c? listesini yükleyin, sonra filtrelemek istedi?iniz kullan?c?y? seçin
5. **Tarih Aral???**: ?ste?e ba?l? olarak ba?lang?ç ve biti? tarihlerini seçin
6. **Merge Filtreleri**: 
   - "Sadece TEST'e merge edilmemi? changesetleri göster" checkbox'?n? i?aretleyin
   - "Sadece MAIN'e merge edilmemi? changesetleri göster" checkbox'?n? i?aretleyin
7. **Ara**: Arama ba?latmak için "Ara" butonuna t?klay?n

## Gerekli Konfigürasyonlar

### 1. TFS Path Yap?s?

Sistem otomatik olarak a?a??daki yap?y? ke?feder:

```
ProductAndDelivery/Destek/
??? BOA/
?   ??? Dev/
?   ??? Test/
?   ??? Main/
??? DigitalBank/
?   ??? Dev/
?   ??? Test/
?   ??? Main/
??? [Di?er Firmalar]/
    ??? Dev/
    ??? Test/
    ??? Main/
```

**Önemli Notlar:**
- Sadece "ProductAndDelivery/Destek" yazman?z yeterli
- Sistem alt?ndaki tüm firma klasörlerini otomatik bulur
- Her firma için Dev, Test, Main branchlerini ke?feder
- Dev branchi olmayan firmalar otomatik olarak atlan?r

### 2. NuGet Paketlerini Yükleme

Projenize a?a??daki NuGet paketlerini yüklemeniz gerekir:

```
Install-Package Microsoft.TeamFoundationServer.Client
Install-Package Microsoft.TeamFoundationServer.ExtendedClient
```

veya Package Manager Console'da:

```powershell
# NSqlTools.BusinessLayer projesine ekleyin
Install-Package Microsoft.TeamFoundationServer.Client -ProjectName NSqlTools.BusinessLayer
Install-Package Microsoft.TeamFoundationServer.ExtendedClient -ProjectName NSqlTools.BusinessLayer
```

### 3. Ana Forma Ekleme

`frmMain.cs` veya ilgili ana form dosyas?nda yeni ekran? menüye ekleyin:

```csharp
// Menü item olu?turma örne?i
ToolStripMenuItem tfsMenuItem = new ToolStripMenuItem("TFS Changeset Arama");
tfsMenuItem.Click += (s, e) => {
    var ucTfs = new ucTfsChangesetSearch();
    // TabPage'e veya panel'e ekleyin
};
```

## Nas?l Çal???r?

1. **Branch Ke?fi**: Kullan?c? `ProductAndDelivery/Destek` yaz?p ara tu?una bast???nda:
   - Sistem bu klasörün alt?ndaki tüm klasörleri listeler (BOA, DigitalBank, vb.)
   - Her klasör için Dev, Test, Main alt klasörlerini kontrol eder
   - Dev branchi bulunan her firma için arama yap?l?r

2. **Changeset Arama**: Her firma için:
   - Dev branchindeki tüm changesetler taran?r
   - Comment ve owner filtrelerine göre sonuçlar süzülür
   - Her changeset için ilgili firman?n Test ve Main branchlerine merge edilip edilmedi?i kontrol edilir

3. **Sonuç Gösterimi**: 
   - Tüm firmalar?n changesetleri tek bir listede gösterilir
   - Branch sütununda hangi firmaya ait oldu?u görülür (örn: BOA/Dev)
   - Merge durumlar? checkbox olarak gösterilir

## Teknik Detaylar

### Dosya Yap?s?

- **NSqlTools.Types\Contracts\TfsChangesetContract.cs**: Changeset veri modeli
- **NSqlTools.Types\FormDataContracts\TfsChangesetSearchScreenDataContract.cs**: Form durumu kayd? için contract
- **NSqlTools.BusinessLayer\Business\TfsBusiness.cs**: TFS API ile ileti?im mant???
- **NSqlTools.UI\Pages\Screens\ucTfsChangesetSearch.cs**: UI kod-behind
- **NSqlTools.UI\Pages\Screens\ucTfsChangesetSearch.Designer.cs**: UI tasar?m dosyas?

### Performans Notlar?

- Branch ke?fi i?lemi ilk aramada yap?l?r ve h?zl?d?r
- Arama i?lemleri BackgroundWorker kullanarak asenkron yap?l?r
- Büyük tarih aral?klar? ve çok say?da firma için arama uzun sürebilir
- Kullan?c? listesi yüklemesi tüm firmalardan kullan?c?lar? toplar
- Her firma için ayr? TFS sorgusu çal??t?r?l?r

## Sorun Giderme

1. **TFS Ba?lant? Hatas?**: 
   - TFS URL'nin do?ru oldu?undan emin olun
   - A? ba?lant?n?z? kontrol edin
   - TFS sunucusuna eri?im izniniz oldu?undan emin olun

2. **Changeset Bulunam?yor**:
   - TFS Path'in do?ru oldu?undan emin olun (örn: ProductAndDelivery/Destek)
   - Alt klasörlerde Dev/Test/Main branchlerinin oldu?unu kontrol edin
   - Tarih aral???n? geni?letin
   - Filtreleri kald?rarak tekrar deneyin

3. **Branch Ke?fi Ba?ar?s?z**:
   - TFS Path yap?n?z?n do?ru oldu?undan emin olun
   - Klasör isimleri case-sensitive olmayabilir (Dev, dev, DEV hepsi çal???r)
   - Alt klasörlere eri?im izniniz oldu?undan emin olun

3. **Merge Durumu Yanl?? Gösteriliyor**:
   - Merge geçmi?i sorgulamas?n?n do?ru çal??t???ndan emin olun
   - Branch adlar?n?n tam olarak e?le?ti?inden emin olun
   - Her firma için ayr? ayr? merge durumu kontrol edilir

## Örnek Kullan?m Senaryosu

**Senaryo**: "ABC-123" task'? için yap?lan tüm changesetleri bul ve hangilerinin henüz TEST'e merge edilmedi?ini gör.

1. TFS URL'i gir: `http://tfs-server:8080/tfs/DefaultCollection`
2. TFS Path'i gir: `ProductAndDelivery/Destek`
3. Comment'e yaz: `ABC-123`
4. "Sadece TEST'e merge edilmemi? changesetleri göster" checkbox'?n? i?aretle
5. "Ara" butonuna t?kla

**Sonuç**: Tüm firmalar?n (BOA, DigitalBank, vb.) Dev branchlerinde "ABC-123" içeren ve henüz TEST'e merge edilmemi? tüm changesetler listelenir.

## ?leride Eklenebilecek Özellikler

- [ ] Changeset detaylar?n? görüntüleme
- [ ] Changeset içindeki dosyalar? listeleme
- [ ] Sonuçlar? Excel'e export etme
- [ ] Merge i?lemi yapabilme
- [ ] Birden fazla branch deste?i
- [ ] Changeset kar??la?t?rma
- [ ] Work Item ba?lant?lar? gösterme
