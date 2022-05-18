
public partial class Pass_Overlap_Calc
{
    private readonly float L, nom_pitch, pass_width;
    private readonly float pitch_tolerance_min, pitch_tolerance_max, length_tolerance_min, length_tolerance_max;
    private readonly int max_iter;

    private static int iteration_cntr;
    private static float pass;
    private float pitch_final;

    private float len, nm_p, pw, ptmin, ptmax, ltmin, ltmax;
    private int cell_num, choice;
    public Pass_Overlap_Calc()
    {
        ParameterInput();

        L = len;
        nom_pitch = nm_p;
        pass_width = pw;
        pitch_tolerance_min = ptmin;
        pitch_tolerance_max = ptmax;
        length_tolerance_min = ltmin;
        length_tolerance_max = ltmax;

        iteration_cntr = 0;
        pass = 0f;
        pitch_final = 0.0f;

        max_iter = 7;
    }

    private void Display_initial()
    {
        Console.Write("\n*******************************************\n");
        Console.Write("This tool calculates the number of passes required for a given length.");
        Console.Write("\n*******************************************\n");
        Console.Write("\n");

        Console.Write("Enter Cell Number (1 or 2): ");
        cell_num = Convert.ToInt16(Console.ReadLine());

        Console.Write("\n\n");

        if (cell_num == 1)
        {
            Console.Write("Default vaules for Cell 1: \n" +
                "-------------------------\n" +
                "nominal pitch = {0}mm\n" +
                "pass width = {1}mm\n" +
                "minimum pitch tolerance = {2}%\n" +
                "maximum pitch tolerance = {3}%\n" +
                "minimum length tolerance = {4}mm\n" +
                "maximum length tolerance = {5}mm\n", 9.525, 12.75, Math.Round(-0.07 * 100), Math.Round(0.04 * 100), -0.5, 0.5);
        }
        else if (cell_num == 2)
        {
            Console.Write("Default vaules for Cell 2: \n" +
                "------------------------\n" +
                "nominal pitch = {0}mm\n" +
                "pass width = {1}mm\n" +
                "minimum pitch tolerance = {2}%\n" +
                "maximum pitch tolerance = {3}\n" +
                "minimum length tolerance = {4}mm\n" +
                "maximum length tolerance = {5}mm\n", 3.2, 6.5, Math.Round(-0.1 * 100), Math.Round(0.1 * 100), -0.5, 0.5);
        }
        else
            Console.WriteLine("Choice error: Cell has not been implemented yet.");

        Console.Write("\n\n");
        Console.WriteLine("Menu:\n" +
            "-----------");
        Console.WriteLine("1 - Input length [mm]\n" +
            "2 - Input length [mm] and nominal pitch [mm]\n" +
            "3 - Input all values\n");
        Console.Write("Choice: ");
        choice = Convert.ToInt16(Console.ReadLine());
        Console.Write("\n");
    }

    private void ParameterInput()
    {
        Display_initial();

        if (cell_num == 1)
        {
            nm_p = 9.525f;
            pw = 12.75f;
            ptmin = -.07f;
            ptmax = .04f;
            ltmin = -.5f;
            ltmax = .5f;
        }

        else if (cell_num == 2)
        {
            nm_p = 3.2f;
            pw = 6.5f;
            ptmin = -.1f;
            ptmax = .1f;
            ltmin = -.5f;
            ltmax = .5f;
        }

        else Console.WriteLine("Error: Cell not implemented yet.");

        switch (choice)
        {
            case 1:
                Console.Write("Length :");
                len = (float)Convert.ToDouble(Console.ReadLine());
                len = (float)Math.Round(len, 2);
                break;

            case 2:
                Console.Write("Length :");
                len = (float)Convert.ToDouble(Console.ReadLine());
                len = (float)Math.Round(len, 2);

                Console.Write("pitch: ");
                nm_p = (float)Convert.ToDouble(Console.ReadLine());
                nm_p = (float)Math.Round(nm_p, 2);
                break;

            case 3:
                Console.Write("Length [mm]:");
                len = (float)Convert.ToDouble(Console.ReadLine());
                len = (float)Math.Round(len, 2);

                Console.Write("Pitch [mm]: ");
                nm_p = (float)Convert.ToDouble(Console.ReadLine());
                nm_p = (float)Math.Round(nm_p, 2);

                Console.Write("Pass width [mm]: ");
                pw = (float)Convert.ToDouble(Console.ReadLine());
                pw = (float)Math.Round(pw, 2);

                Console.Write("Pitch tolerance % min: ");
                ptmin = (float)Convert.ToDouble(Console.ReadLine());
                ptmin /= 100f;
                ptmin = (float)Math.Round(ptmin, 2);

                Console.Write("Pitch tolerance % max: ");
                ptmax = (float)Convert.ToDouble(Console.ReadLine());
                ptmax /= 100f;
                ptmax = (float)Math.Round(ptmax, 2);

                Console.Write("Length tolerance [mm] min: ");
                ltmin = (float)Convert.ToDouble(Console.ReadLine());
                ltmin = (float)Math.Round(ltmin, 2);

                Console.Write("Length tolerance [mm] max: ");
                ltmax = (float)Convert.ToDouble(Console.ReadLine());
                ltmax = (float)Math.Round(ltmax, 2);

                break;

            default:
                Console.WriteLine("Switch case error: invalid option.");
                break;
        }       
            
    }
}



class Program_Main
{
    public static void Main()
    {
        Pass_Overlap_Calc calc = new();
        calc.Calculate();
        calc.DisplaySolution();

        Console.WriteLine("\n\nPress enter to exit...");
        Console.ReadLine();
    }
}