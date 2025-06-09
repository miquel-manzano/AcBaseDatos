using cat.itb.M6UF2EA1_Sol.cruds;

namespace cat.itb.M6UF2EA1_Sol;
internal class Program
    {
        public static void Main()
        {
            var program = new Program();
            program.Start();
        }

        private void Start()
        {
            bool end;
            do
            {
                PrintMenu();
                end = ChooseOption();
            } while (!end);
        }

        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- MENU ---");
            Console.ResetColor();
            Console.WriteLine("\n[0] Clean Database School" +
                              "\n[1] Exercici 1"  +
                              "\n[2] Exercici 2" +
                              "\n[3] Exercici 3" +
                              "\n[4] Exercici 4" +
                              "\n[5] Exercici 5" +
                              "\n[6] Exercici 6" +
                              "\n[7] Exercici 7" +
                              "\n[8] Exercici 8" +
                              "\n[9] Exit");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n   -- Introduce an option");
            Console.ResetColor();
        }
        
        private static void CleanDB()
        {
            GeneralCRUD gcrud = new GeneralCRUD();
            gcrud.DropTable("notas");
            gcrud.DropTable("alumnos");
            gcrud.DropTable("asignaturas");
            gcrud.ExecuteScriptSchool();
        }
        
        private static void ExerciseTitle(int exercise)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- Exercise {0} ---\n", exercise);
            Console.ResetColor();
        }

        private bool ChooseOption()
        {
            var acrud = new AlumnosCRUD();
            var ncrud = new NotasCRUD();
            bool isValidOption;
            do
            {
                isValidOption = true;
                string option = Console.ReadLine();
                if (option != "9") Console.Clear();
                switch (option)
                {
                    case "0":
                        CleanDB();
                        break;
                    case "1":
                        ExerciseTitle(1);
                        acrud.SelectAll();
                        break;
                    case "2":
                        ExerciseTitle(2);
                        ncrud.SelectAll();
                        break;
                    case "3":
                        ExerciseTitle(3);
                        ncrud.SelectMarksFromPupil("4448242");
                        break;
                    case "4":
                        ExerciseTitle(4);
                        acrud.insert("33873557A", "Ramom Badaula","C/ Vilagrassa, 22","Seva","674521332");
                        acrud.insert("23873557B", "Josep Muñoz","C/ Vilajoiosa, 33","Balaguer","674521222");
                        acrud.insert("13873557C", "Sara Pellin","C/ Vilaseca, 44","Flix","674521334");
                        break;
                    case "5":
                        ExerciseTitle(5);
                        List<string> dnis = new List<string>();
                        List<int> codis = new List<int>();
                        dnis.Add("33873557A");
                        dnis.Add("23873557B");
                        dnis.Add("13873557C");
            
                        codis.Add(4);
                        codis.Add(5);
            
                        ncrud.InsertMaks(dnis,codis,8);
                        break;
                    case "6":
                        ExerciseTitle(6);
                    /*
                        List<int> codis2 = new List<int>();
                        codis2.Add(4);
                        codis2.Add(5);
                       ncrud.UpdateMarks("Cerrato Vela, Luis", codis2, 9);
                    */
                    List<string> subjects = new List<string>();
                    subjects.Add("FOL");
                    subjects.Add("RET");
                    ncrud.UpdateMarks2("Cerrato Vela, Luis", subjects, 9);
                    break;
                    case "7":
                        ExerciseTitle(7);
                        acrud.UpdatePhone("12344345", "934885237");
                        break;
                    case "8":
                        ExerciseTitle(8);
                        acrud.DeletePupilsFromCity("Mostoles");
                        break;
                    case "9":
                        return true;
                    default:
                        PrintMenu();
                        Console.WriteLine("\nIntroduce a valid option:");
                        isValidOption = false;
                        break;
                }
            } while (!isValidOption);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n     -- Press any key to return to the menu");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
    }
