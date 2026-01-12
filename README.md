# 🧟‍♂️ 3D Zombie Survival v Unity (Maturitní projekt)

Tento projekt je 3D survival střílečka vytvořená v rámci maturitní práce na **SSPU Opava** (Školní rok 2025/26). Hra kombinuje prvky akčního FPS a survival žánru, kde hráč čelí vlnám zombie nepřátel s cílem dosáhnout co nejvyššího skóre

## 🎮 Klíčové mechaniky

* **Pohybový systém:** Implementován pomocí `Rigidbody` pro realistickou fyziku. Zahrnuje chůzi, skok (s cooldownem 0.5s), a krčení.
* **Systém staminy:** Sprintování spotřebovává výdrž, která se vizualizuje na UI liště a po 4 sekundách od ukončení běhu se automaticky regeneruje.
* **Soubojový systém:** * **Zbraň:** Stylizovaná low-poly laserová pistole využívající technologii **Raycast** pro detekci zásahů.
    * **Parametry:** Zombíci mají 3 životy, hráč začíná se 100 životy. 
    * **Rychlost střelby:** Cooldown mezi výstřely je 0.6 sekundy.
* **AI Nepřátel:** Zombie využívají **NavMesh** pro inteligentní navigaci a vyhýbání se překážkám. Mají dva režimy: pronásledování (vzdálenost > 1.7m) a útok.
* **Progresivní obtížnost:** Každé druhé zabití zombie trvale zvýší rychlost všech nepřátel o 0.2 jednotky.

## 🛠️ Použité technologie a assety

* **Engine:** Unity 2021.3 LTS
* **Skriptování:** C# (Visual Studio 2022)
* **Assety:**
    * `Lowpoly Environment - Nature Free`: Modely stromů a vegetace (v mapě je cca 5000 stromů).
    * `Low Poly Zombie`: Riggovaný model se 3 animacemi ze Sketchfabu.
    * `Pistol`: Model z Poly Pizza.
    * `Zvuk`: "Plasma - Lazer Pistol Gun Shot 1" (Freesound.org).

## 🗺️ Herní svět

Mapa o rozměrech 500x500 jednotek byla vytvořena pomocí **Unity Terrain Tools**. Pro lepší atmosféru a optimalizaci výkonu (omezení renderování v dálce) je ve hře implementována dynamická světle modrá mlha.

## 🕹️ Ovládání

| Klávesa | Akce |
| :--- | :--- |
| **WASD / Šipky** | Pohyb postavy |
| **Levý Shift** | Sprint (žere staminu) |
| **Mezerník** | Skok |
| **Levý Ctrl** | Krčení |
| **Levé myšítko** | Střelba z laserové pistole |
| **Myš** | Rozhlížení |
| **Escape** | Návrat do hlavního menu |

## 📊 Systém skóre

[cite_start]Za každého eliminovaného zombie získá hráč 1 bod. Nejvyšší dosažené skóre se ukládá pomocí `PlayerPrefs`, takže zůstává uloženo i po vypnutí hry.

---
**Autor:** Marián Kufa (IT4)
**Škola:** Střední škola průmyslová a umělecká, Opava
