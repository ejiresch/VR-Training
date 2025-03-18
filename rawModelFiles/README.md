## Alle 3D-Modelle

In diesem Ordner sind alle 3D-Modelle, die mit dem Raum was zu tun haben. Also Möbel, wie Bett, Nachttisch (Tooltisch), etc. und der Raum selbst.

Ebenfalls sind die aktuellen (meist) FBX-Dateien schon vorhanden.

Die Texturen sind im Blender File schon verpackt.

#### Wie mit den 3D-Modellen gearbeitet werden soll

Änderungen an den Möbel soll nur bei den bestimmten .blend Datei passieren.
Beispiel:

Es muss was am Krankenbett geändert werden. Daher werden die Änderungen **nur** an bett.blend stattfinden und **nicht** in raum.blend.

Dieser wird dann als FBX exportiert oder alles wird kopiert und in raum.blend eingefügt.

Nachdem alles passt kann dann der ganze Raum exportiert werden und in Unity ersetzt werden.

###### Wo in Unity wird es exportiert?

**Für 3D-Modelle**

VR-Training > Assets > Models

**Für Texturen**

VR-Training > Assets > Textures

#### Wie werden Texturen mit exportiert?

Texturen werden nicht in der FBX Dater mit exportiert.

Um die Texturen zu bekommen, muss man diese entpacken.

File > External Data > Uncheck Automatically Pack Resources > Unpack Resources > Use files in current directory (create when necessary)

<img src="https://files.horizon.pics/e94af3ff-3b6a-4a90-a17b-ac29a14c46c4?a=582&mime1=image&mime2=jpeg" title="" alt="asd" width="271">
