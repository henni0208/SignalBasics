# SignalBasics – Erklärung und Bedienung

## Worum geht es?

Dieses Programm ist ein Lern- und Übungstool für die Grundlagen der Nachrichtentechnik und Elektrotechnik.  
Das Programm hilft dabei, Kopfrechnen zu trainieren und das Verständnis für die Zusammenhänge zwischen Frequenz, Wellenlänge, Periodendauer, dB, dBm, Leistung und Spannung zu überprüfen.

Das Programm stellt zufällige oder gezielte Aufgaben, bei denen Werte umgerechnet und in verschiedenen Einheiten angegeben werden müssen.  
Die Eingaben werden auf Richtigkeit und Einheit geprüft, inklusive Toleranzbereich.

## Verwendung & Ausführung
Dieses Dokument stellt nur den Quellcode des Programms bereit.
Um das Programm auszuführen, muss der Code in eine geeignete Entwicklungsumgebung wie z. B. Visual Studio eingefügt und als C# Konsolenanwendung gestartet werden:

**Schritte:**

- Visual Studio öffnen und ein neues Projekt erstellen.

- Projekttyp: Konsolenanwendung (.NET / C#) auswählen.

- Den bereitgestellten Quellcode in die automatisch erstellte Program.cs einfügen.

- Projekt ausführen (F5 oder "Starten").



## Funktionen

- **Frequenz, Wellenlänge, Zeit:**  
  Umrechnung und Abfrage von Frequenz, Wellenlänge und Periodendauer.

- **dB und dBm:**  
  Umrechnung und Abfrage von Pegelwerten (dB, dBm) sowie deren Bezug zu Leistung (W, mW) und Spannung (V).

- **Einheiten und Präfixe:**  
  Das Programm erkennt und verarbeitet verschiedene Einheiten und Größenordnungen (z.B. k, M, G, m, µ/u, n, p).

- **Toleranz:**  
  Die Eingaben werden mit einer Toleranz von ±20% akzeptiert.

## Bedienung

1. **Starten:**  
   Das Programm startet eine Konsole, in der alle stattfindet.

2. **Menü:**  
   Wählen Sie eine Option aus dem Menü (z.B. Frequenz abfragen, dBm abfragen).

3. **Aufgaben lösen:**  
   Geben Sie die gefragten Werte ein, Größsenordnung egal. (z.B. `2.5 kHz`, `-3 dBm`, `1.2 µV` oder `1.2 uV`)

4. **Beenden:**  
   Das Programm kann jederzeit über das Menü beendet werden.

## Hinweise zur Eingabe

- Dezimalzahlen können mit Punkt oder Komma eingegeben werden.
- Einheiten und Präfixe können direkt hinter die Zahl geschrieben werden oder mit Leerzeichen. (z.B. `5mW`, `10kHz`, `2uV`)
- Für Mikro kann sowohl `µ` als auch `u` verwendet werden.
- Negative Werte sind möglich (z.B. `-10 dBm`).

## Kontakt

Für Fragen oder Feedback wenden Sie sich bitte an:  
**Hendrik**  
E-Mail: hw035191@fh-muenster.de

---

© 2025 Hendrik Woltering. Alle Rechte vorbehalten.
