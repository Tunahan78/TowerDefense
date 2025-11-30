# TowerDefense




https://github.com/user-attachments/assets/5ec224d6-5f58-43f8-adcb-89a85212d648





# 🏰 SOLID Mimarisine Uygun Tower Defense (TD) Projesi

Bu proje, temel bir 3D Tower Defense (Kule Savunması) oyunu geliştirmek için atılmış adımları içermektedir. Projenin ana amacı, sadece çalışan bir oyun yapmak değil, aynı zamanda **SOLID prensiplerini**, **Tasarım Desenlerini (Design Patterns)** ve **Genişletilebilir Yazılım Mimarisi** standartlarını uygulamaktır.

## 🚀 Mimarinin Temel Prensipleri

Bu projenin kod tabanı, gelecekteki genişlemeler (yeni kuleler, düşmanlar, yetenekler) için esnek ve sürdürülebilir olacak şekilde tasarlanmıştır.

| Prensip | Uygulama | Faydası |
| :--- | :--- | :--- |
| **Tek Sorumluluk (SRP)** | Her bileşen tek bir iş yapar. Örn: `HeadRotator` sadece dönmekten, `TowerAttacker` sadece atış zamanlamasından sorumludur. | Kod, modüler ve hata ayıklaması kolaydır. |
| **Açık/Kapalı (OCP)** | Yeni kule türleri (Lazer, Havan) eklemek, mevcut `TowerAttacker` kodunu **değiştirmeyi gerektirmez**. | Proje, sürdürülebilirdir ve genişlemeye açıktır. |
| **Bağımlılık Tersine Çevirme (DIP)** | Üst düzey bileşenler (örn: `TowerAttacker`), somut sınıflara değil, **arayüzlere** (`IAttackBehavior`, `ITargetingStrategy`) bağımlıdır. | Bileşenler birbirine sıkı sıkıya bağlı değildir (Loosely Coupled). |

## 🛠️ Uygulanan Tasarım Desenleri ve Yapılar

| Desen / Yapı | Amaç | Uygulama |
| :--- | :--- | :--- |
| **Strategy Pattern** | Davranış takası (Behavior Swapping). | **`IAttackBehavior`** (Lazer vs. Mermi) ve **`ITargetingStrategy`** (En Yakın vs. Gelişmiş Hedefleme) sınıfları. |
| **Component-Based** | Düşman/Kule yeteneklerini ayırmak. | Tüm birimler `HealthComponent`, `EnemyMovement`, `TargetingComponent` gibi bağımsız parçalardan oluşur. |
| **Factory Pattern** | Nesne yaratımını merkezileştirmek. | **`TowerFactoryBehavior`** sınıfı, `TowerData`'yı okuyarak kuleye doğru saldırı mekaniğini ve görsel efektleri enjekte eder. |
| **Scriptable Objects** | Veriyi koddan ayırmak. | **`TowerDataSO`** ve **`EnemyDataSO`** dosyaları, tüm istatistik ve yapılandırma verilerini tutar. |

## 🧠 Gelişmiş Hedefleme Stratejisi

Projede kullanılan **`AdvancedTargetingStrategy`** (Zeki Hedefleme) sınıfı, kulelerin basit mesafe yerine, aşağıdaki hiyerarşiye göre karar vermesini sağlar:

1.  **Öncelik (Dominant):** Bitiş çizgisine en yakın olan düşman (`IPathProgress` verisi).
2.  **Eşitlik Bozucu:** Aynı mesafedeki düşmanlar arasında en yüksek tehdit skoruna sahip olan (`IThreatLevel` verisi).
3.  **Stabilite:** Kuleler, küçük skor dalgalanmalarında hedefi bırakmamak için **`STABILITY_BONUS`** ile mevcut hedeflerine sadık kalır.

## 📝 Kurulum ve Başlatma

1.  Projeyi klonlayın.
2.  Unity (LTS Sürümü Önerilir) ile açın.
3.  `SampleScene` sahnesini açın.
4.  Oyun, önceden tanımlanmış dalgalarla otomatik olarak başlayacaktır.
