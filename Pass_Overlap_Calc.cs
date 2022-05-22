

public partial class Pass_Overlap_Calc
{
    private void CalculatePasses()
    {
        /*Calculates the initial number of passes and pitch using length, nominal pitch and pass width.*/

        float pitch_temp;

        pass = (length - pass_width) / nom_pitch;
        pass = (float)Math.Round(pass, 0) + 1;

        pass = (float)Math.Round(pass);
        pitch_temp = Get_Pitch();

        iteration_cntr = 1;
        pitch_final = CalculatePitch(pitch_temp);
        pitch_final = (float)Math.Round(pitch_final, 3);
    }

    private float CalculatePitch(float pitch_calc)
    { 
        /*Calculates the pitch that falls within the tolerance. If the initial calculation is 
         * not in tolerance, the function recursively optimises upto a specified limit till the 
         * solution converges.*/

        float error_pitch, error_length, error_diff = 0.0f ;

        // Calculate initial pitch and length error
        error_pitch = Get_PitchError(pitch_calc);
        error_length = Get_LengthError(pitch_calc);

        // If pitch and length are within tolerance, return value.
        if (Pitch_InTolerance(error_pitch) && Length_InTolerance(error_length)) return pitch_calc;



        // Get the error difference in pitch with respect to the tolerance range.
        if (error_pitch <= pitch_tolerance_min) error_diff = pitch_tolerance_min - error_pitch;

        else if (error_pitch >= pitch_tolerance_max) error_diff = error_pitch - pitch_tolerance_max;



        // Recalculate the pitch using the error difference.
        pitch_calc = pitch_calc * (1 + error_diff);
        pitch_calc = (float)Math.Round(pitch_calc, 2);


        // Calculate pitch and length error
        error_pitch = Get_PitchError(pitch_calc);
        error_length = Get_LengthError(pitch_calc);


        // If pitch and length are within tolerance, return the calculated value.
        if (Pitch_InTolerance(error_pitch) && Length_InTolerance(error_length)) return pitch_calc;


        // If length is out of tolerance, change the number of passes.
        if (error_length >= length_tolerance_max && pass > 0) pass -= 1;
        else if (error_length <= length_tolerance_min) pass += 1;


        // Escape with 0.0 if no. of iterations are at limit. Prevents stack overflow. 
        if (iteration_cntr >= max_iter) return 0.0f;


        // Recurse with a recalculated pitch.
        iteration_cntr += 1;
        return CalculatePitch(Get_Pitch());
    }



    private float Get_PitchError(float pitch_calc)
    {
        /*Returns the normalised pitch error from (pitch_calc) input.*/

        float error_pitch = (pitch_calc - nom_pitch) / nom_pitch;
        return (float)Math.Round(error_pitch, 2);
    }

    private float Get_LengthError(float pitch_calc)
    {
        /*Returns the length error using length calculated from (pitch_calc) pitch input.*/

        float error_length = ((pitch_calc * (pass - 1)) + pass_width) - length;
        return (float)Math.Round(error_length, 2);
    }

    private float Get_Pitch()
    {
        /*Returns the calculated pitch using length, pass width, and passes.*/

        return (length - pass_width) / (pass - 1);
    }




    private bool Pitch_InTolerance(float delta)
    {
        /*Returns TRUE if the normalised pitch error (delta) is within specified tolerance. FALSE if otherwise.*/

        delta = (float)Math.Round(delta, 2);
        return (delta >= pitch_tolerance_min) && (delta <= pitch_tolerance_max);
    }

    private bool Length_InTolerance(float delta)
    {
        /*Returns TRUE if the length error (delta) is within specified tolerance. FALSE if otherwise.*/

        delta = (float)Math.Round(delta, 2);
        return (delta >= length_tolerance_min) && (delta <= length_tolerance_max);
    }





    public void DisplaySolution()
    {
        /*Displays the number of passes, pitch, and errors.*/

        float error_pitch, error_length;

        error_pitch = (pitch_final - nom_pitch) / nom_pitch;
        error_pitch = (float)Math.Round(error_pitch, 3);

        error_length = Math.Abs(((pitch_final * (pass-1)) + pass_width) - length);
        error_length = (float)Math.Round(error_length, 2);

        Console.Write("\n\n");
        Console.Write("\n\t*************************************\t\n\n");

        if (pitch_final > 0.0f)
        {
            Console.WriteLine("{0} passes at {1}mm pitch, {2}% pitch error with {3}mm error in length.", pass, pitch_final, error_pitch * 100, error_length);
            Console.WriteLine("\nSolution converged in {0} iteration(s).", iteration_cntr);
        }
        else
        {
            Console.WriteLine("\nCalculation error: Solution failed to converge in {0} (max) iterations.", iteration_cntr);
        }
        Console.Write("\n\t*************************************\t\n");
    }

    public void Calculate()
    {
        /*Creates an abstraction that encapsulates all the calculations within a public method.*/

        CalculatePasses();
    }
}
