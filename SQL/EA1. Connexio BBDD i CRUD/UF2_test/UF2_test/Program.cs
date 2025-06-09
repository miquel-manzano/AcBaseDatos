
using UF2_test.cruds;

class Program
{
   
    public static void Main(string[] args)
    {
        var acrud = new AlumnosCRUD();
        var ncrud = new NotasCRUD();
        var gcrud = new GeneralCRUD();

        Console.WriteLine("\n[0] Clean Database" +
                              "\n[1] Exercici 1"  +
                              "\n[2] Exercici 2" +
                              "\n[3] Exercici 3" +
                              "\n[4] Exercici 4" +
                              "\n[5] Exercici 5" +
                              "\n[6] Exercici 6" +
                              "\n[7] Exercici 7" +
                              "\n[8] Exercici 8" +
                              "\n[9] Exit");

        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 0:
                gcrud.DropTable("notas");
                gcrud.DropTable("alumnos");
                gcrud.DropTable("asignaturas");
                gcrud.ExecuteScriptSchool();
                break;
            case 1:
                Console.WriteLine("---Ex.1 Mostrar taula Alumnos---");
                acrud.SelectAll();
                Console.WriteLine("---");
                break;
            case 2:
                Console.WriteLine("---Ex.2 Mostrar taula Notas---");
                ncrud.SelectAll();
                Console.WriteLine("---");
                break;
            case 3:
                Console.WriteLine("---Ex.3 Mostrar les notes de l’alumne amb DNI 4448242 de la taula NOTAS utilitzant el Prepared Statement.---");
                ncrud.SelectByDNI("4448242");
                Console.WriteLine("---");
                break;
            case 4:
                Console.WriteLine("---Ex.4 Insertar 3 alumnes nous. Inventat les dades dels alumnes nous.---");
                acrud.Insert("33873557A", "Ramom Badaula", "C/ Vilagrassa, 22", "Seva", "674521332");
                acrud.Insert("23873557B", "Josep Muñoz", "C/ Vilajoiosa, 33", "Balaguer", "674521222");
                acrud.Insert("13873557C", "Sara Pellin", "C/ Vilaseca, 44", "Flix", "674521334");
                Console.WriteLine("---");
                break;
            case 5:
                Console.WriteLine("---Ex.5 Insertar les notes per aquests 3 nous alumnes de les assignatures FOL i RET. Tots han tret un 8 en les dues assignatures. Utilitza el Prepared Statement.---");
                ncrud.Insert("33873557A", 4, 8);
                ncrud.Insert("23873557B", 4, 8);
                ncrud.Insert("13873557C", 4, 8);
                Console.WriteLine("---");
                break;
            case 6:
                Console.WriteLine("---Ex.6 Modificar les notes de l’alumne \"Cerrato Vela, Luis\" de FOL i RET, ha tret un 9.---");
                List<string> subjects = new List<string>();
                subjects.Add("FOL");
                subjects.Add("RET");
                ncrud.Update2("Cerrato Vela, Luis", subjects, 9);
                Console.WriteLine("---");
                break;
            case 7:
                Console.WriteLine("---Ex.7 Modificar el teléfon de l’alumne amb DNI = 12344345, el nou teléfon és 934885237.---");
                acrud.UpdatePhone("12344345", "934885237");
                Console.WriteLine("---");
                break;
            case 8:
                Console.WriteLine("---Ex.8 Eliminar l’alumne que viu a \"Mostoles\".---");
                acrud.DeletePupilsFromCity("Mostoles");
                Console.WriteLine("---");
                break;
            case 9:
                Console.WriteLine("Good night...");
                break;
        }        
    }
}


