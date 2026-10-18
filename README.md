# 3D Sci-Fi Room Progression & Action Prototype

[us English](#english) | [tr Türkçe](#türkçe)

<a name="english"></a>
## English

This project is a modular 3D sci-fi action and room progression prototype developed with modern Unity standards. The primary focus of the project is to implement a clean, decoupled architecture using feature-based folder organization and custom performance-optimized shaders.

### 🏛️ Architectural Overview

The project follows a **Feature-based architecture**, moving away from traditional "Manager-heavy" structures[cite: 4]. Each system (Player States, Lasers, Rooms, Checkpoints) operates independently with clear boundaries of responsibility.

#### Technologies & Core Principles
* **State Pattern:** Player logic is structured using a robust Hierarchical/Modular State Machine (`IdleState`, `MoveState`, `SprintState`, `DeadState`, etc.).
* **MaterialPropertyBlock (MPB):** Utilized for dynamic barrier opacity transitions without breaking GPU instancing across multiple instances.
* **DOTween Workflow:** Standardized smooth UI and gameplay animations using tweens and sequences.
* **Custom Shaders:** Fresnel and refraction-based barrier shaders built specifically for the sci-fi environment.
* **Asset Creation:** All 3D environment models, room assets, and interactive elements were custom-modelled in Blender.

### 📂 Project Organization

The script architecture and folder hierarchy reflect a clean, modular workspace:

* **Core:** Global management systems (`CameraManager`, `CheckPointManager`, `VFXManager`), events, and constants (`GameConstant`).
* **Features:** Modular gameplay components categorized by domain:
  * **Room:** Room controllers, enter/pass triggers, and barriers.
  * **Laser:** Rotators, laser beams, and interactive laser controllers.
  * **Player:** State machine classes, inputs, and UI controllers (`PlayerStaminaUI`, `PlayerInteractView`).
* **Interface:** Contracts such as `IState` and `ILaserTargetable`.

---

<a name="türkçe"></a>
## Türkçe

Bu proje, modern Unity standartları kullanılarak geliştirilmiş, modüler bir 3D bilimkurgu oda ilerleme ve aksiyon prototipidir. Projenin temel odak noktası; özellik bazlı (feature-based) klasör yapısı, özel optimizasyonlar ve tamamen kendi tasarrufumuz olan shader’lar ile sürdürülebilir, temiz bir mimari kurmaktır.

### 🏛️ Mimari Yapı

Proje, geleneksel "Manager" odaklı şişkin yapılardan arındırılarak **Özellik Bazlı (Feature-based)** bir mimari ile inşa edilmiştir[cite: 4]. Böylece her sistem (Player State'leri, Lazerler, Odalar, Checkpoint'ler) kendi sorumluluk alanına ve bağımsız sınırlarına sahiptir.

#### Kullanılan Teknolojiler & Prensipler
* **State Pattern:** Karakter hareket ve aksiyon mekanikleri modüler bir State Machine yapısıyla (`IdleState`, `MoveState`, `SprintState`, vb.) yönetilmektedir.
* **MaterialPropertyBlock (MPB):** Çoklu bariyer örneklerinde GPU instancing bozulmadan dinamik şeffaflık (opacity) geçişleri sağlanmıştır.
* **DOTween Entegrasyonu:** Pürüzsüz UI ve nesne animasyonları için DOTween sequence yapıları standartlaştırılmıştır.
* **Özel Shader'lar:** Bilimkurgu atmosferi için özel olarak geliştirilmiş Fresnel ve kırılma tabanlı bariyer shader'ları kullanılmıştır.
* **3D Modelleme:** Projedeki tüm 3D çevre modelleri, oda objeleri ve interaktif unsurlar Blender kullanılarak özel olarak tasarlanmıştır.

### 📂 Proje Organizasyonu

Kod tabanı ve klasör hiyerarşisi düzenli ve kolay okunabilir bir çalışma alanı sunar:

* **Core:** Genel yönetim sistemleri (`CameraManager`, `CheckPointManager`, `VFXManager`), event'ler ve sabitler (`GameConstant`).
* **Features:** Alanlarına göre ayrılmış modüler oyun mekanikleri:
  * **Room:** Oda kontrolcüleri, giriş/geçiş tetikleyicileri ve bariyerler.
  * **Laser:** Lazer rotatörleri, ışınlar ve interaktif lazer kontrolcüm sistemleri.
  * **Player:** State machine sınıfları, girdi yönetimi ve UI bileşenleri (`PlayerStaminaUI`, `PlayerInteractView`).
* **Interface:** `IState` ve `ILaserTargetable` gibi ortak arayüz sözleşmeleri.
