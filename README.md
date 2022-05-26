# Pass Calculator

**A tool to calculate the number of passes required for cladding or welding on a surface to cover a particular length.**  



## A. User Guide

### 1. Enter Cell Number
```
Enter Cell Number (1 / 2 / 3) : 
```
Enter the cell number. This is to set the default values for the lasers particular to each cell. Choosing a cell will display its default values. Enter **1** for Cell-1, **2** for Cell-2, and **3** for Cell-3.

### 2. Choose from Menu
A menu will be displayed with the options to input the required parameters.

- Option 1. Input Length : Input only the length in mm, use default values for all other parameters.
- Option 2. Input length and nominal pitch : Input length in mm and the nominal pitch in mm. Use default values for all other parameters.
- Option 3. Input all values : Input all required parametres and override the default values. Leaving a field blank and pressing Enter will use the default value for that field. Therefore, only the values that need to be changed can be explicitly entered.

In the field,
```
Choice : 
```
Enter the number corresponding to the choice based on the menu. Then enter the values as prompted on the screen. ***DO NOT ENTER UNITS. NUMBERS ONLY***


#### The pass calculator uses the following parameters:
 
 - **length** : The length in millimetres of the surface to be clad / welded.
 - **nominal pitch** : The normal pass-to-pass pitch in millimetres.
 - **pass width** : The width of a single pass in millimetres.
 - **pitch tolerance min** : The *lower limit* of the tolerance on the pitch. This is how much lower the pitch can deviate from the nominal value. The tolerance is a positive or negative percentage (%) value.
 - **pitch tolerance max** : The *upper limit* tolerance on the pitch.  This is how much higher the pitch can deviate from the nominal value. The tolerance is a positive or negative percentage (%) value.
 - **length tolerance min** : The *lower limit*, in millimetres, of the deviation of the final length, as a result of the calculated number of passes and pitch.
 - **length tolerance max** : The *upper limit*, in millimetres, of the deviation of the final length, as a result of the calculated number of passes and pitch. 


### 3. Done
After entering the required parameters (described in step 2), the solution will be displayed.





## B. Example
Here is a simple example on how to calculate the number of passes for a length of 550mm.


When the console application is opened, the screen shows the following:
```
            ****   P A S S    C A L C U L A T O R   ****

This tool calculates the number of passes required for a given length.

**********************************************************************

Enter Cell Number (1 / 2 / 3):
```

Choosing Cell 1, 

```
Enter Cell Number (1 / 2 / 3): 1
```

On pressing enter to confirm the choice, the following information is presented on the screen:
```
Default vaules for Cell 1:
-------------------------
nominal pitch = 9.525mm
pass width = 12mm
minimum pitch tolerance = -7%
maximum pitch tolerance = 4%
minimum length tolerance = -0.5mm
maximum length tolerance = 0.5mm


Menu:
-----------
1 - Input length [mm]
2 - Input length [mm] and nominal pitch [mm]
3 - Input all values

Choice:
```

Enter 1 to input only the length and keep all other values to defaults.

```
Choice: 1
```

The length will now need to be input by the user in millimetres:
```
Length: 550
```
On pressing enter, the final solution is displayed; which is the number of passes, the calculated pitch, as well as the error in length. It also shows the number of iterations it took for the calculation to converge.
```
        *************************************

57 passes at 9.607mm pitch, 0.9% pitch error with 0.01mm error in length.

Solution converged in 0 iteration(s).

        *************************************
```



## C. Using the Tolerances to "bias" the Calculations:
Depending on the application, it may be sometimes desireable to bias the pitch to be "tighter" or more spread apart. This is possible by changing the tolerance values on the pitch. Making the tolerance values more negative will reduce the top limit on the pitch and shorten it, while making the tolerance more positive will have the opposite effect on the pitch.

Here is the same calculation shown in the example with 550mm length, but with an exaggerated tolerance bias of 5% minimum limit and 10% maximum limit:
```
Choice: 3

Input length. For other parameters, leaving them blank will use default values.

Length [mm]: 550
Pitch [mm]:
Pass width [mm]:
Pitch tolerance [%] min: 5
Pitch tolerance [%] max: 10
Length tolerance [mm] min:
Length tolerance [mm] max:



        *************************************

54 passes at 10.151mm pitch, 6.6% pitch error with 0mm error in length.

Solution converged in 3 iteration(s).

        *************************************
```


### Divergent Solutions
Note that in the main example in section B, the solution converged in 0 iteration, while in the example with the bised tolerances in section C, the solution converged in 3 iterations. There may be situations for certain lengths or tolerances that the solution diverges, i.e., there is no solution for the specified tolerances. 

Here is the same example from section B, but with a tolerance bias of -10% minimum limit and -1% maximum:
```
Choice: 3

Input length. For other parameters, leaving them blank will use default values.

Length [mm]: 550
Pitch [mm]:
Pass width [mm]:
Pitch tolerance [%] min: -10
Pitch tolerance [%] max: -1
Length tolerance [mm] min:
Length tolerance [mm] max:



        *************************************


Calculation error: Solution failed to converge in 10 (max) iterations.

        *************************************
```

The maximum iterations is set to 10 to prevent overflow errors.
