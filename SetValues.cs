
public partial class Pass_Overlap_Calc
{
    private readonly float length, nom_pitch, pass_width;
    private readonly float pitch_tolerance_min, pitch_tolerance_max, length_tolerance_min, length_tolerance_max;
    private readonly int max_iter;

    private static int iteration_cntr;

    /// <summary>
    /// Number of passes.
    /// </summary>
    public static float pass;

    /// <summary>
    /// The optimsed pitch between passes.
    /// </summary>
    public float pitch_final;

    /// <summary>
    /// Instantiate a <c>Pass_Overlap_Calc</c> object.
    /// </summary>
    /// <param name="length_"></param>
    /// <param name="nom_pitch_"></param>
    /// <param name="pass_width_"></param>
    /// <param name="pitch_tolerance_min_"></param>
    /// <param name="pitch_tolerance_max_"></param>
    /// <param name="length_tolerance_min_"></param>
    /// <param name="length_tolerance_max_"></param>
    public Pass_Overlap_Calc(float length_, float nom_pitch_, float pass_width_, float pitch_tolerance_min_, float pitch_tolerance_max_, float length_tolerance_min_, float length_tolerance_max_)
    {
        length = length_;
        nom_pitch = nom_pitch_; // nominal pitch
        pass_width = pass_width_;
        pitch_tolerance_min = pitch_tolerance_min_;
        pitch_tolerance_max = pitch_tolerance_max_;
        length_tolerance_min = length_tolerance_min_;
        length_tolerance_max = length_tolerance_max_;

        iteration_cntr = 0;
        pass = 0f;
        pitch_final = 0.0f;

        // Hardcoded constant value for maximum no. of iterations.
        max_iter = 20;
    }
}


/// <summary>
/// Generate a menu style input on the console.
/// </summary>
public class Inputs
{
    /// <summary>
    /// Create a new <c>Inputs</c> object.
    /// </summary>
    public Inputs() { }

    /// <summary>
    /// len = Length [mm],
    /// nm_p = Nominal Pitch [mm],
    /// pw = Pass Width [mm],
    /// ptmin = Minimum Pitch Tolerance [-1 to 1],
    /// ptmax = Maximum Pitch Tolerance [-1 to 1],
    /// ltmin = Minimum Length Tolerance [mm],
    /// ltmax = Maximum Length Tolerance [mm],
    /// </summary>
    public float len, nm_p, pw, ptmin, ptmax, ltmin, ltmax;
    private int cell_num, choice;

    private void MenuInput()
    {
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
    private void Display_defaults(int cell_num)
    {
        if (cell_num == 1 || cell_num == 2 || cell_num == 3)
        {
            Console.Write("Default values for Cell {0}: \n" +
                "-------------------------\n" +
                "nominal pitch = {1}mm\n" +
                "pass width = {2}mm\n" +
                "minimum pitch tolerance = {3}%\n" +
                "maximum pitch tolerance = {4}%\n" +
                "minimum length tolerance = {5}mm\n" +
                "maximum length tolerance = {6}mm\n", cell_num, nm_p, pw, Math.Round(ptmin * 100), Math.Round(ptmax * 100), ltmin, ltmax);

            if(cell_num == 2 || cell_num == 3)
            {
                Console.WriteLine("\n\nnote: pass width is taken as equivalent to aiming beam width for cell {0}.", cell_num);
            }
        }
        else
            Console.WriteLine("Invalid choice.");
    }


    private void ParameterInput()
    {
        string? temp;

        Console.Write("\n\n");
        Console.Write("\t\t****   P A S S    C A L C U L A T O R   ****\n\n");
        Console.Write("This tool calculates the number of passes required for a given length.");
        Console.Write("\n\n**********************************************************************\n");
        Console.Write("\n");

        Console.Write("Enter Cell Number (1 / 2 / 3): ");
        cell_num = Convert.ToInt16(Console.ReadLine());

        Console.Write("\n\n");
        

        SetDefaults(cell_num);
        Display_defaults(cell_num);

        MenuInput();


        switch (choice)
        {
            case 1:
                Console.WriteLine("Input numeric values only.\n");
                Console.Write("Length: ");
                len = (float)Convert.ToDouble(Console.ReadLine());
                len = (float)Math.Round(len, 2);
                break;

            case 2:
                Console.WriteLine("Input length. For other parameters, leaving them blank will use default values.\n");
                Console.WriteLine("Input numeric values only.\n");

                Console.Write("Length: ");
                len = (float)Convert.ToDouble(Console.ReadLine());
                len = (float)Math.Round(len, 2);

                Console.Write("pitch: ");
                nm_p = (float)Convert.ToDouble(Console.ReadLine());
                nm_p = (float)Math.Round(nm_p, 2);
                break;

            case 3:
                Console.WriteLine("Input length. For other parameters, leaving them blank will use default values.\n");
                Console.WriteLine("Input numeric values only.\n");

                Console.Write("Length [mm]: ");
                len = (float)Convert.ToDouble(Console.ReadLine());
                len = (float)Math.Round(len, 2);

                Console.Write("Pitch [mm]: ");
                temp = Console.ReadLine();
                if (temp.Equals(string.Empty) == false)
                {
                    nm_p = (float)Convert.ToDouble(temp);
                    nm_p = (float)Math.Round(nm_p, 3);
                }

                Console.Write("Pass width [mm]: ");
                temp = Console.ReadLine();
                if (temp.Equals(string.Empty) == false)
                {
                    pw = (float)Convert.ToDouble(temp);
                    pw = (float)Math.Round(pw, 2);
                }

                Console.Write("Pitch tolerance [%] min: ");
                temp = Console.ReadLine();
                if (temp.Equals(string.Empty) == false)
                {
                    ptmin = (float)Convert.ToDouble(temp);
                    ptmin /= 100f;
                    ptmin = (float)Math.Round(ptmin, 2);
                }

                Console.Write("Pitch tolerance [%] max: ");
                temp = Console.ReadLine();
                if (temp.Equals(string.Empty) == false)
                {
                    ptmax = (float)Convert.ToDouble(temp);
                    ptmax /= 100f;
                    ptmax = (float)Math.Round(ptmax, 2);
                }

                Console.Write("Length tolerance [mm] min: ");
                temp = Console.ReadLine();
                if (temp.Equals(string.Empty) == false)
                {
                    ltmin = (float)Convert.ToDouble(temp);
                    ltmin = (float)Math.Round(ltmin, 2);
                }

                Console.Write("Length tolerance [mm] max: ");
                temp = Console.ReadLine();
                if (temp.Equals(string.Empty) == false)
                {
                    ltmax = (float)Convert.ToDouble(temp);
                    ltmax = (float)Math.Round(ltmax, 2);
                }

                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }

    }

    private void SetDefaults(int cell_num)
    {
        // Default values for cells.
        if (cell_num == 1)
        {
            nm_p = 9.525f;
            pw = 12f;
            ptmin = -.07f;
            ptmax = .04f;
            ltmin = -.5f;
            ltmax = .5f;
        }
        else if (cell_num == 2)
        {
            nm_p = 3.2f;
            pw = 5.0f;
            ptmin = -.1f;
            ptmax = .1f;
            ltmin = -.5f;
            ltmax = .5f;
        }
        else if (cell_num == 3)
        {
            nm_p = 2.8f;
            pw = 2.0f;
            ptmin = -.1f;
            ptmax = .1f;
            ltmin = -.5f;
            ltmax = .5f;
        }
        else throw new Exception("Invalid Input");
    }


    /// <summary>
    /// Gets the required inputs from the user in a menu style format.
    /// </summary>
    public void GetInput()
    {
        ParameterInput();
    }
}



class Program_Main
{
    public static void Main()
    {
        Inputs inputs = new();

        while (true)
        {
            try
            {
                inputs.GetInput();

                Pass_Overlap_Calc calc = new(inputs.len, inputs.nm_p, inputs.pw, inputs.ptmin, inputs.ptmax, inputs.ltmin, inputs.ltmax);
                calc.Calculate();
                calc.DisplaySolution();

                Console.Write("\n\nPress Enter to continue or type 'n' to exit : ");
                string? s = Console.ReadLine();

                if (s.ToLower().Equals("n")) break;
            }
            catch
            {
                Console.WriteLine("\n*** Error ***\n");
                Console.WriteLine("\n\nException encountered. Try again.\n\n\n");
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}