using Microsoft.VisualBasic.FileIO;
using System;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

class Program
{
    private static float[] frequencies = new float[100];
    private static float[] wavelengths = new float[100];
    private static float[] times = new float[100];
    private static float[] db = new float[80];
    private static float[] leistungsVersaetzung = new float[80];
    private static float[] spannungsVersaetzung = new float[80];
    private static float[] spannungsDaempfung = new float[80];
    private static float[] leistungsDaempfung = new float[80];
    static void Main(string[] args)
    {
        Init();
        do
        {
            Menu();
        }
        while (InputCheck());
        Credits();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    private static void Init()
    {
        for (int i = 0; i < 75; i++)
        {
            db[i] = i;
        }
        int e = 4;
        for (int i = 75; i < (75 + 5); i++)
        {
            db[i] = e * 10;
            e++;
        }
        for (int i = 0; i < 80; i++)
        {
            leistungsDaempfung[i] = (float)Math.Pow(10, -db[i] / 10); // Faktor
            leistungsVersaetzung[i] = (float)Math.Pow(10, db[i] / 10);

            spannungsDaempfung[i] = (float)Math.Pow(10, -db[i] / 20);
            spannungsVersaetzung[i] = (float)Math.Pow(10, db[i] / 20);
        }
        for (int i = 0; i < 100; i++)
        {
            frequencies[i] = (float)Math.Pow(10, i / 10.0); // Frequenzwerte von 1 Hz bis 100 GHz
            wavelengths[i] = 299792458 / frequencies[i]; // Wellenlängen in Metern
            times[i] = 1 / frequencies[i]; // Periodendauer in Sekunden
        }
    }
    private static void Credits()
    {
        Console.Clear();
        Console.WriteLine("This program was created by Hendrik");
        Console.WriteLine("Copyright (c) 2025 Hendrik. All rights reserved.");
        Console.WriteLine("No liability is assumed for any errors or inaccuracies in this program.");
        Console.WriteLine("This program is provided 'as is' without any warranty, express or implied.");
        Console.WriteLine("For any questions or concerns, please contact me at hw035191@fh-muenster.de");
        Console.WriteLine("");
    }
    private static bool InputCheck()
    {
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int option))
        {
            switch (option)
            {
                case 1:
                    while (RandomTest()) ;
                    break;
                case 2:
                    while (Frequenz()) ;
                    break;
                case 3:
                    while (Wellenlaenge()) ;
                    break;
                case 4:
                    while (Zeit()) ;
                    break;
                case 5:
                    while (DB()) ;
                    break;
                case 6:
                    while (DBM()) ;
                    break;
                case 7:
                    Credits();
                    Console.ReadKey();
                    break;
                case 8:
                    return false;
                default:
                    Console.WriteLine("Ungültige Option. Bitte wählen Sie eine gültige Option.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl ein.");
        }
        Console.WriteLine("");
        return true;
    }
    private static void Menu()
    {
        Console.Clear();
        Console.WriteLine("----\t\tMenu\t\t----");
        Console.WriteLine("\t1. Random abfragen");
        Console.WriteLine("\t2. Frequenz abfragen");
        Console.WriteLine("\t3. Wellenlänge abfragen");
        Console.WriteLine("\t4. Zeit abfragen");
        Console.WriteLine("\t5. dB abfragen");
        Console.WriteLine("\t6. dBm abfragen");
        Console.WriteLine("\t7. Credits");
        Console.WriteLine("\t8. Beenden");
        Console.WriteLine("------------------------------------");
    }
    private static bool Weiter()
    {
        Console.WriteLine("Weiter (Y/N)?");
        string? input = Console.ReadLine();
        if (input != null)
        {
            string inputLower = input.ToLower();
            if (inputLower == "yes" || inputLower == "y" || inputLower == "ja" || inputLower == "j" || inputLower == "ye")
            {
                return true;
            }
            return false;
        }
        return true;
    }
    private static bool RandomTest()
    {
        int random = Random.Shared.Next(0, 5);//0-4
        switch (random)
        {
            case 0:
                return Frequenz();
            case 1:
                return Wellenlaenge();
            case 2:
                return Zeit();
            case 3:
                return DB();
            case 4:
                return DBM();
            default: //passiert nie
                return false;
        }
    }
    private static (float value, string order, string unit) ParseValue(string input)
    {
        input = input.Trim();
        input = input.Replace(" ", "");
        input = input.Replace(',', '.');
        string[] splits = input.Split(".");

        bool m_as_meters_not_as_mili_check = input.Count(c => c == 'm') == 1 && input.All(c => !char.IsLetter(c) || c == 'm');
        // Regulärer Ausdruck, um die Zahl und die Einheit zu extrahieren
        var regex = new Regex(@"^(-?\d+(?:\.\d+)?)\s*(m|k|M|G|µ|u|n|p)?\s*(dbm|db|W|m|s|Hz|V)?$", RegexOptions.IgnoreCase);
        var match = regex.Match(input);

        

        if (!match.Success)
        {
            throw new ArgumentException("Ungültige Eingabe");
        }

        // Zahl extrahieren
        var value = float.Parse(match.Groups[1].Value);

        // Größenordnung extrahieren
        var order = match.Groups[2].Value;
        if (string.IsNullOrEmpty(order))
        {
            order = "";
        }

        // Einheit extrahieren
        var unit = match.Groups[3].Value;
        if (string.IsNullOrEmpty(unit))
        {
            unit = "";
        }
        if (splits.Length > 1)
        {
            string zeros = "";
            int leading = CountLeadingZeros(splits[1]);
            for (int i = 0; i < leading; i++)
            {
                zeros += "0";
            }
            string corrected_value = splits[0] + "," + zeros + value.ToString().Replace(splits[0], "");
            value = float.Parse(corrected_value);
        }
        if (m_as_meters_not_as_mili_check)
        {
            order = "";
            unit = "m";
        }
        return (value, order, unit);
    }
    private static int CountLeadingZeros(string str)
    {
        int count = 0;
        foreach (char c in str)
        {
            if (c == '0')
            {
                count++;
            }
            else
            {
                break; // Stoppe, wenn ein anderes Zeichen gefunden wird
            }
        }
        return count;
    }
    private static bool Frequenz()
    {
        Console.Clear();
        Console.WriteLine("Gesucht: Frequenz");
        int randomType = Random.Shared.Next(0, 2); // 0 oder 1
        int randomNumber = Random.Shared.Next(0, 100); //zwischen 0 und 99
        if (randomType == 0)
        {
            //Wellenlaenge zu Frequenz
            (string einheit, float zahl) freq = GetOptimalUnit(frequencies[randomNumber]);
            (string einheit, float zahl) wave = GetOptimalUnit(wavelengths[randomNumber]);
            Console.WriteLine($"Wellenlänge: {wave.zahl} {wave.einheit}m enspricht?");
            while (!Check(freq.zahl, "Hz", freq.einheit)) ;
        }
        else
        {
            //Zeit/Dauer zu Frequenz
            (string einheit, float zahl) freq = GetOptimalUnit(frequencies[randomNumber]);
            (string einheit, float zahl) time = GetOptimalUnit(times[randomNumber]);
            Console.WriteLine($"Periodendauer: {time.zahl} {time.einheit}s enspricht?");
            while (!Check(freq.zahl, "Hz", freq.einheit)) ;
        }

        return Weiter();
    }
    private static (string einheit, float zahl) GetOptimalUnit(float value)
    {
        string unit = "";
        float newValue = value;

        if (value >= 1000000000)
        {
            unit = "G";
            newValue = value / 1000000000;
        }
        else if (value >= 1000000)
        {
            unit = "M";
            newValue = value / 1000000;
        }
        else if (value >= 1000)
        {
            unit = "k";
            newValue = value / 1000;
        }
        else if (value >= 1)
        {
            unit = "";
            newValue = value;
        }
        else if (value >= 0.001)
        {
            unit = "m";
            newValue = (float)(value / 0.001);
        }
        else if (value >= 0.000001)
        {
            unit = "µ";
            newValue = (float)(value / 0.000001);
        }
        else if (value >= 0.000000001)
        {
            unit = "n";
            newValue = (float)(value / 0.000000001);
        }
        else if (value >= 0.000000000001)
        {
            unit = "p";
            newValue = (float)(value / 0.000000000001);
        }

        return (unit, newValue);
    }
    private static bool Check(float ist, string einheit, string groesse)
    {
        float ist_backup = ist;
        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("Bitte tätigen Sie eine Eingabe!");
            return false;
        }
        try
        {
            (float value, string order, string unit) ergebnis = ParseValue(input);
            if (ergebnis.order != groesse)
            {
                switch (groesse)
                {
                    case "m":
                        ist *= 0.001f;
                        break;
                    case "k":
                        ist *= 1000f;
                        break;
                    case "M":
                        ist *= 1000000f;
                        break;
                    case "G":
                        ist *= 1000000000f;
                        break;
                    case "µ":
                    case "u":
                        ist *= 0.000001f;
                        break;
                    case "n":
                        ist *= 0.000000001f;
                        break;
                    case "p":
                        ist *= 0.000000000001f;
                        break;
                }

                switch (ergebnis.order)
                {
                    case "m":
                        ist /= 0.001f;
                        break;
                    case "k":
                        ist /= 1000f;
                        break;
                    case "M":
                        ist /= 1000000f;
                        break;
                    case "G":
                        ist /= 1000000000f;
                        break;
                    case "µ":
                    case "u":
                        ist /= 0.000001f;
                        break;
                    case "n":
                        ist /= 0.000000001f;
                        break;
                    case "p":
                        ist /= 0.000000000001f;
                        break;
                }
            }
            if (ergebnis.unit != einheit)
            {
                Console.WriteLine("Falsche Einheit");
                return false;
            }
            // Mit 20% Toleranz vergleichen
            float tolerance = Math.Abs(ist) * 0.2f;
            if (Math.Abs(ergebnis.value - ist) <= tolerance)
            {
                Console.WriteLine($"Richtig! Ihre Eingabe: {ergebnis.value} {ergebnis.order}{ergebnis.unit}");
            }
            else
            {
                Console.WriteLine($"Falsch! Ihre Eingabe: {ergebnis.value} {ergebnis.order}{ergebnis.unit}");
            }

        }

        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        Console.WriteLine($"Genauer Wert: {ist_backup} {groesse}{einheit}");
        return true;
    }
    private static bool Wellenlaenge()
    {
        Console.Clear();
        Console.WriteLine("Gesucht: Wellenlänge");
        int randomType = Random.Shared.Next(0, 2); // 0 oder 1
        int randomNumber = Random.Shared.Next(0, 100); //zwischen 0 und 99
        if (randomType == 0)
        {
            //Frequenz zu Wellenlaenge
            (string einheit, float zahl) freq = GetOptimalUnit(frequencies[randomNumber]);
            (string einheit, float zahl) wave = GetOptimalUnit(wavelengths[randomNumber]);
            Console.WriteLine($"Frequenz: {freq.zahl} {freq.einheit}Hz enspricht?");
            while (!Check(wave.zahl, "m", wave.einheit)) ;
        }
        else
        {
            //Zeit/Dauer zu Wellenlange
            (string einheit, float zahl) wave = GetOptimalUnit(wavelengths[randomNumber]);
            (string einheit, float zahl) time = GetOptimalUnit(times[randomNumber]);
            Console.WriteLine($"Periodendauer: {time.zahl} {time.einheit}s enspricht?");
            while (!Check(wave.zahl, "m", wave.einheit)) ;
        }

        return Weiter();
    }
    private static bool Zeit()
    {
        Console.Clear();
        Console.WriteLine("Gesucht: Periodendauer");
        int randomType = Random.Shared.Next(0, 2); // 0 oder 1
        int randomNumber = Random.Shared.Next(0, 100); //zwischen 0 und 99
        if (randomType == 0)
        {
            //Wellenlaenge zu Zeit 
            (string einheit, float zahl) time = GetOptimalUnit(times[randomNumber]);
            (string einheit, float zahl) wave = GetOptimalUnit(wavelengths[randomNumber]);
            Console.WriteLine($"Wellenlänge: {wave.zahl} {wave.einheit}m enspricht?");
            while (!Check(time.zahl, "s", time.einheit)) ;
        }
        else
        {
            //Frequenz zu Zeit
            (string einheit, float zahl) freq = GetOptimalUnit(frequencies[randomNumber]);
            (string einheit, float zahl) time = GetOptimalUnit(times[randomNumber]);
            Console.WriteLine($"Frequenz: {freq.zahl} {freq.einheit}Hz enspricht?");
            while (!Check(time.zahl, "s", time.einheit)) ;
        }

        return Weiter();
    }
    private static bool DB()
    {
        Console.Clear();
        int random = Random.Shared.Next(0, 2);//0 oder 1
        int randomNumber = Random.Shared.Next(0, 80);
        int type2 = Random.Shared.Next(0, 2);//0 oder 1
        if (random == 0)
        {
            Console.WriteLine("Gesucht: Volt");
            //von dbm zu Watt   
            if (type2 == 0)
            {
                Console.WriteLine("Gedämpft");
                (string einheit, float zahl) db_ = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) watt_daempf = GetOptimalUnit(spannungsDaempfung[randomNumber]);
                Console.WriteLine($"dB: -{db_.zahl} {db_.einheit}dB enspricht?");
                while (!Check(watt_daempf.zahl, "V", watt_daempf.einheit)) ;
            }
            else
            {
                Console.WriteLine("Verstärkt");
                (string einheit, float zahl) db_ = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) watt_verst = GetOptimalUnit(spannungsVersaetzung[randomNumber]);
                Console.WriteLine($"dB: {db_.zahl} {db_.einheit}dB enspricht?");
                while (!Check(watt_verst.zahl, "V", watt_verst.einheit)) ;
            }
        }
        else
        {
            Console.WriteLine("Gesucht: dB");
            //von watt zu DBM
            if (type2 == 0)
            {
                Console.WriteLine("Gedämpft");
                (string einheit, float zahl) db_ = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) volt_daempf = GetOptimalUnit(spannungsDaempfung[randomNumber]);
                Console.WriteLine($"Volt: {volt_daempf.zahl} {volt_daempf.einheit}V enspricht?");
                while (!Check(-db_.zahl, "dB", db_.einheit)) ;
            }
            else
            {
                Console.WriteLine("Verstärkt");
                (string einheit, float zahl) db_ = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) volt_verst = GetOptimalUnit(spannungsVersaetzung[randomNumber]);
                Console.WriteLine($"Volt: {volt_verst.zahl} {volt_verst.einheit}V enspricht?");
                while (!Check(db_.zahl, "dB", db_.einheit)) ;
            }
        }

        return Weiter();
    }
    private static bool DBM()
    {
        Console.Clear();
        int random = Random.Shared.Next(0, 2);//0 oder 1
        int randomNumber = Random.Shared.Next(0, 80);
        int type2 = Random.Shared.Next(0, 2);//0 oder 1
        if (random == 0)
        {
            Console.WriteLine("Gesucht: Leistung");
            //von dBm zu mW   
            if (type2 == 0)
            {
                Console.WriteLine("Gedämpft");
                (string einheit, float zahl) dbm = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) watt_daempf = GetOptimalUnit(leistungsDaempfung[randomNumber] / 1000f);
                Console.WriteLine($"dBm: -{dbm.zahl} {dbm.einheit}dBm enspricht?");
                while (!Check(watt_daempf.zahl, "W", watt_daempf.einheit)) ;
            }
            else
            {
                Console.WriteLine("Verstärkt");
                (string einheit, float zahl) dbm = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) watt_verst = GetOptimalUnit(leistungsVersaetzung[randomNumber] / 1000f);
                Console.WriteLine($"dBm: {dbm.zahl} {dbm.einheit}dBm enspricht?");
                while (!Check(watt_verst.zahl, "W", watt_verst.einheit)) ;
            }
        }
        else
        {
            Console.WriteLine("Gesucht: dBm");
            //von mW zu dBm
            if (type2 == 0)
            {
                Console.WriteLine("Gedämpft");
                (string einheit, float zahl) dbm = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) watt_daempf = GetOptimalUnit(leistungsDaempfung[randomNumber] / 1000f);
                Console.WriteLine($"Leistung: {watt_daempf.zahl} {watt_daempf.einheit}W enspricht?");
                while (!Check(-dbm.zahl, "dBm", dbm.einheit)) ;
            }
            else
            {
                Console.WriteLine("Verstärkt");
                (string einheit, float zahl) dbm = GetOptimalUnit(db[randomNumber]);
                (string einheit, float zahl) watt_verst = GetOptimalUnit(leistungsVersaetzung[randomNumber] / 1000f);
                Console.WriteLine($"Leistung: {watt_verst.zahl} {watt_verst.einheit}W enspricht?");
                while (!Check(dbm.zahl, "dBm", dbm.einheit)) ;
            }
        }

        return Weiter();
    }

}