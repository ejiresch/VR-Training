# README

## Lighting

Wenn du dafür verantwortlich bist, das Lighting zu ändern/fixen bist du hier richtig.

Von Jiresch empfohlenes Tutorial, falls man nicht lesen möchte:

https://www.youtube.com/watch?v=VnG2gOKV9dw&ab_channel=Brackeys

### Static und Dynamic Lighting

Was heißen diese Sachen überhaupt?

**Static:** 

- Lighting das auf dem PC des Entwicklers vorberechnet wurde und auf die Textur der 3D-Modelle gespeichert wird. 
- Nicht für bewegbare Objekte geeignet (bzw. überhaupt anwendbar). 
- Viel hübscher für weniger Leistung
- Muss manuell neuberechnet werden, wenn betroffene Gegenstände vom Entwickler neubewegt werden 

Tutorial wie man es berechnet findet ihr unten unter "Static Lighting berechnen"

**Dynamic:**

- Lighting wird Live beim Spielen auf dem SpielerPC berechnet. (In jedem Frame)
- Funktioniert für alle Objekte (Auch Bewegbare)
- Kostet mehr Leistung für ein nicht so schönes Ergebnis
- Licht Gameobjects in Unity (directional Light, Area Light, etc.) sind standartmässig dynamic
- Alles funktioniert automatisch, sobald das Light platziert wurde.

### Warum verwenden wir beides?

Static Lighting sieht besser aus für weniger Leistung, aber es funktioniert nur auf statischen (also unbewegbaren) Gameobjects.

Für die bewegbaren Gameobjects brauchen wir ein dynamic Light, wofür derzeit ein einziges directional Light verwendet wird. Wenn die bewegbaren Gegenstände nicht von einem dynamic Light angeleuchtet werden, sind sie einfach komplett schwarz. Die Culling Mask beim dynamic Light hat nur die Layers Interactables, Player und NoColliderOff aktiv und beleuchtet somit nur Gameobjects in diesen Layers. (NoColliderOff ist nur für den Ballon der Kanuele)

Dynamic Lights sollten den Shadow Type "No Shadows" haben. Schatten verbrauchen zu viel Leistung und der Spieler merkt die meist gar nicht.

### Static Lighting berechnen

- Lighting Fenster unter Window>Rendering>Lighting öffnen
  - Für static Lighting ist der Unterpunkt Lightmapping Settings zuständig
- Unten Auto Generate ausschalten falls es an ist (Nervt ansonsten wenn man Gameobjects bewegen will)

- Unten auf Generate Lighting klicken und abwarten

**Unsere Settings:**

| Settingname                   | Value                     |
| ----------------------------- | ------------------------- |
| Lightmapper                   | Progressive GPU (Preview) |
| Progressive Updates           | ✅                         |
| Multiple Importance Sampling  | ✅                         |
| Direct Samples                | 32                        |
| Indirect Samples              | 512                       |
| Environment Samples           | 256                       |
| Light Probe Sample Multiplier | 4                         |
| Min Bounces                   | 2                         |
| Max Bounces                   | 8                         |
| Filtering                     | Auto                      |
| Indirect Resolution           | 2 (Locked)                |
| Lightmap Resolution           | 100                       |
| Lightmap Padding              | 2                         |
| Max Lightmap Size             | 1024                      |
| Lightmap Compression          | High Quality              |
| Ambient Occlusion             | ❌                         |
| Directional Mode              | Directional               |
| Albedo Boost                  | 1                         |
| Indirect Intensity            | 1                         |
| Lightmap Parameters           | Default-Medium            |

Diese können auch abgeändert werden. Wir haben uns für diese Settings entschieden, weil sie gut genug aussehen, aber dafür nicht extrem lange zum Berechnen brauchen.

### Warum funktioniert das static Lighting nicht?

**Ein Gameobject wird nicht beleuchtet:** 

Damit ein Gameobject vom static Lighting betroffen werden kann, muss es selber auch static sein -> Gameobject anklicken > In der oberen rechten Ecke vom Inspector die "static" Checkbox anhaken.

**Ich kann die Lighting Einstellungen nicht ändern:**

Höchstwahrscheinlich ist kein Lighting Settings Asset ausgewählt. Das sollte die erste Einstellung im Lightingfenster sein. Wenn dort "None" steht, einfach rechts aufs Kreissymbol klicken und eins auswählen. Wenn keins existiert, einfach auf New Lighting Settings klicken (Achtung das setzt die Einstellungen auf Default zurück).

**Das Licht erzeugt merkwürdige Artefakte/ist rosa:**

Experimentiert mit den Settings herum bzw. den Punkt "Das Licht wird nicht fertigt/richtig berechnet" probieren. Wenn das nicht reicht kann googeln vom genauen Problem besser helfen.

**Das Licht wird nicht fertig/richtig berechnet:**

Auf den Pfeil bei Generate Lighting klicken > Clear Baked Data > Generate Lighting

Das sollte meisten helfen, falls das Generieren buggy ist.