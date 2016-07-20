using System;

namespace PrintSpiral
{
    class Program
    {
        static void Main()
        {
            int n = 3;
            int[][] mySquare2DArray =
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 9},
            };

            n = 4;
            mySquare2DArray = new[]
            {
                new[] {1, 2, 3, 4},
                new[] {5, 6, 7, 8},
                new[] {9, 10, 11, 12},
                new[] {13, 14, 15, 16},
            };

            n = 5;
            mySquare2DArray = new[]
            {
                new[] {1, 2, 3, 4, 5},
                new[] {6, 7, 8, 9, 10},
                new[] {11, 12, 13, 14, 15},
                new[] {16, 17, 18, 19, 20},
                new[] {21, 22, 23, 24, 25},
            };

            n = 1;
            mySquare2DArray = new[]
            {
                new[] {1}
            };

            n = 0;
            mySquare2DArray = new int[0][];

            PrintSpiral(mySquare2DArray, n);
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        static void PrintSpiral(int[][] array, int n)
        {
            int passes = (int) Math.Ceiling(n/2.0);
            for (int pass = 0; pass < passes; pass++)
            {
                PrintPass(array, n, pass);
            }
            Console.WriteLine();
        }

        static void ValidatePass(int n, int pass)
        {
            // The pass number must not be a negative number.
            if (pass < 0)
                throw new ArgumentOutOfRangeException(nameof(pass), "A negative pass over the array is not allowed.");

            // Validate the pass number so we can avoid out-of-bounds errors.
            int passes = (int) Math.Ceiling(n/2.0);
            if (pass > passes)
                throw new ArgumentOutOfRangeException(nameof(pass),
                    "The pass over the array is larger than the current array size requires.");
        }

        static void PrintPass(int[][] array, int n, int pass)
        {
            ValidatePass(n, pass);

            // Print the forward pass, which is the top row of values and the right side,
            // excluding the values from the last row and the left side of the 2d array.
            PrintForwardPass(array, n, pass);

            // Now print the return pass, which is the bottom row of values, printed from right to left (logically).
            // We also need to print the values from the left side of the 2d array, from the last row of the pass to
            // the first row, excluding the values from the indices already printed.
            PrintReturnPass(array, n, pass);
        }

        static void PrintForwardPass(int[][] array, int n, int pass)
        {
            ValidatePass(n, pass);

            // The first row of pass 0 is row 0.
            // The first row of pass 1 is row 1. Etc...
            int row = pass;

            // The first row of pass 0 needs values from 0 up to n printed.
            // The first row of pass 1 needs values from 1 up to n-1 printed.
            // So the first row of pass p needs values from p up to n-p printed.
            for (int column = pass; column < n - pass; column++)
            {
                Console.Write(array[row][column] + ", ");
            }

            // The rows after the first row and before the last row must only print one number.
            // (Make sure to avoid the last row, since that is printed in the return pass.)
            // The forward pass of pass 0 excludes row n.
            // The forward pass of pass 1 excludes row n-1.
            // The forward pass of pass p excludes row n-p.
            // We add one to pass in the first expression of the for loop
            // since the code above already handles the first row.
            for (row = pass + 1; row < n - pass - 1; row++)
            {
                // The last value in the row for pass 0 is the value before n, which is n-1.
                // The last value in the row for pass 1 is the value before n-1, which is n-2.
                // The last value in the row for pass p is the value before n-p, which is n-p-1.
                Console.Write(array[row][n - pass - 1] + ", ");
            }
        }

        static void PrintReturnPass(int[][] array, int n, int pass)
        {
            ValidatePass(n, pass);

            // Because the arrays are n x n (square), if n is even, the last pass of the spirally printing
            // the array will be a complete pass with a forward and backward pass.  For a 2 x 2 array, the
            // first forward pass will print [0][0], [0][1], [1][1] and the first return pass will print [1][0].
            // If n is an odd number, than the last pass will only have a forward pass and no return pass.
            // For a 3 x 3 array, the first forward pass will print [0][0], [0][1], [0][2], [1][2], [2][2]
            // and the first return pass will print [2][1], [2][0], [1][0].  The second forward pass will
            // print [1][1], which is the last value of the array.  There will be nothing left for the return
            // pass to print.  So if we have an odd value for n, the last return pass can simply return since
            // it has no work to do.  Otherwise, process the return pass.
            if (pass == n/2 && n%2 > 0) return;

            // The last row for pass 0 is the row before n (exclusive), which is n-1.
            // The last row for pass 1 is the row before n-1 (exclusive), which is n-2.
            // The last row for pass p is the row before n-p (exclusive), which is n-p-1.
            int row = n - pass - 1;

            // The starting row of return pass 0 needs values printed from before n (exclusive), which is n-1, to 0 (inclusive).
            // The starting row of return pass 1 needs values printed from before n-1 (exclusive), which is n-2, to 1 (inclusive).
            // The starting row of return pass p needs values printed from before n-p (exclusive), which is n-p-1, to p (inclusive).
            for (int column = n - pass - 1; column >= pass; column--)
            {
                Console.Write(array[row][column] + ", ");
            }

            // To complete the return pass, we must print values for each row starting at the row prior to the last row
            // (excluded since we printed it above) and ending at but not including the first row of the pass.  The last
            // row we print is the starting row from the forward pass +1.

            // The starting row for the return pass is the row prior to the last row.

            // The ending row for return pass 0 is 1.
            // The ending row for return pass 1 is 2.
            // The ending row for return pass p is p+1.
            for (row = row - 1; row > pass; row--)
            {
                // For pass 0, column 0 needs to be printed.
                // For pass 1, column 1 needs to be printed.
                // For pass p, column p needs to be printed.
                Console.Write(array[row][pass] + ", ");
            }
        }
    }
}