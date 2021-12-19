using System;
using System.Collections.Generic;

namespace MinutesCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.realMain();
            //myProgram.test1();
            //myProgram.test2();

        }

        public void realMain()
        {
            bool running;
            int dotsFound; // stores the number of periods found in the input string, which should be 2
            
            string input;
            string minuteHolder;
            string secondHolder;
            string msHolder;
            MTime holderTime;
            MTime finalTime;
            List<MTime> timesArray = new List<MTime>();

            int noOfTimes = 0; // keeps track of how many times were entered
            int totalMs = 0; // the sum of all the times, in ms

            //Console.WriteLine(args[0]); //command line arguments example
            Console.WriteLine("Welcome to minutes calculator.");
            Console.WriteLine("Enter times in the form of 'xx.yy.zz' (x being minutes, y seconds, z milliseconds).");
            Console.WriteLine("After each time, hit enter. When done, type 'd', then enter.");

            running = true;
            while (running)
            {
                dotsFound = 0;
                minuteHolder = "";
                secondHolder = "";
                msHolder = "";
                holderTime = null;
                input = Console.ReadLine();
                if (input == "d") // the user quit the application
                {
                    running = false;
                }
                else if (input == "t") // test stuff
                {
                    Console.WriteLine("testing");
                    this.test1();
                    running = false;
                }
                else // the user entered a time
                {
                    //Console.WriteLine(input);

                    foreach (char c in input)
                    {
                        if (Char.IsDigit(c)) //checks that c is a number
                        {
                            //Console.WriteLine("c is a digit");
                            if (dotsFound == 0) // no periods, add the number to minutes
                            {
                                minuteHolder = minuteHolder + c;
                            } 
                            else if (dotsFound == 1) // one period, add the number to seconds
                            {
                                secondHolder = secondHolder + c;
                            }
                            else if (dotsFound == 2) // two periods, add the number to milliseconds
                            {
                                msHolder = msHolder + c;
                            }
                            else // error, invalid number of periods
                            {
                                Console.WriteLine("error, there can only be exactly 2 periods in an input, quitting");
                                running = false;
                                break;
                            }
                        }
                        else if (c == '.') // checks if c is a period
                        {
                            dotsFound++;
                            //Console.WriteLine("c is a .");
                        }
                        else // c is not a number, program quits due to error
                        {
                            Console.WriteLine("c is invalid, quitting");
                            running = false;
                            break;
                        }
                    }
                    
                    holderTime = new MTime();
                    holderTime.Minutes = Int32.Parse(minuteHolder);
                    holderTime.Seconds = Int32.Parse(secondHolder);
                    holderTime.Milliseconds = Int32.Parse(msHolder);

                    //this line runs and proves that the 3 holders are successfully getting their data
                    //Console.WriteLine($"Time: {holderTime.Minutes}.{holderTime.Seconds}.{holderTime.Milliseconds}");
                    totalMs = totalMs + convertToMs(holderTime);
                    timesArray.Add(holderTime);

                    noOfTimes++;
                }
            }

            //outputs all times in the list
            foreach (MTime mt in timesArray)
            {
                Console.WriteLine($"Time: {mt.Minutes}.{mt.Seconds}.{mt.Milliseconds}");
                Console.WriteLine($"In milliseconds: {convertToMs(mt)}");
            }

            finalTime = new MTime();
            finalTime = convertMsToMTime(totalMs);
            Console.WriteLine("\nFinal result");
            //Console.WriteLine($"Total ms: {totalMs}");
            //Console.WriteLine($"Time: {convertMTimeToFormatted(finalTime)}");
            Console.WriteLine($"Time: {finalTime.Minutes}.{finalTime.Seconds}.{finalTime.Milliseconds}");
            //Console.WriteLine($"Time: {convertWithTimespan(totalMs)}");
        }

        public int convertToMs(MTime mt1)
        {
            int ms = 0;

            ms = ms + mt1.Milliseconds;
            ms = ms + (mt1.Seconds * 100);
            ms = ms + (mt1.Minutes * 60000);

            return ms;
        }

        public MTime convertMsToMTime(int i1)
        {
            MTime mt2 = new MTime();

            mt2.Minutes = (i1 / 60000); //divides the total ms by 60000 to find the number of full minutes
            i1 = (i1 % 60000);

            mt2.Seconds = (i1 / 100); //divides the remaining ms by 100 to find the number of full seconds
            while (mt2.Seconds >= 60)
            {
                mt2.Seconds -= 60;
                mt2.Minutes += 1;
            }
            i1 = (i1 % 100);

            mt2.Milliseconds = i1; //adds the remaining ms to the milliseconds field

            return mt2;
        }

        public void test1()
        {
            MTime mtime = new MTime();
            mtime.Minutes = 7;
            mtime.Seconds = 77;
            mtime.Milliseconds = 77;
            Console.WriteLine("Test Method");
            Console.WriteLine($"Time: {mtime.Minutes}.{mtime.Seconds}.{mtime.Milliseconds}");
        }

        public void test2() // learning how remainders work
        {
            Console.WriteLine("Remainder Test Method");
            Console.WriteLine(18 / 7);
        }
    }
}
