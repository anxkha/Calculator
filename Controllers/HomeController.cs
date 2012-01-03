// ----------------------------------------------------------------------------
//  HomeController.cs
//  Copyright (c) 2012, Lucas M. Suggs
//
//  This is the Controller for the calculator. There is only one View, so only
//  one controller.
// ----------------------------------------------------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using Calculator.Models;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Create a default Model instance.
            Models.Calculator c = new Models.Calculator();

            c.CurrentValue = 0;
            c.CurrentHistory = "";
            c.CurrentOperation = Models.Calculator.Operations.None;
            c.LastValue = 0;
            c.RunningTotal = 0;
            c.FirstOperation = true;
            c.DoEqualsRepeat = false;
            c.DivideByZero = false;

            // Stuff the Model instance into the current session, so we can
            // have multiple users using this at the same time.
            Session["Model"] = c;

            return View(c);
        }
        
        [HttpPost]
        public ActionResult Index(Models.Calculator m)
        {
            Models.Calculator c = (Models.Calculator)Session["Model"];

            /*string[] delims = new string[]{
                " + ",
                " - ",
                " * ",
                " / "
            };*/

            // We reset the DivideByZero flag every postback, as the postback
            // might solve the problem and we don't want to display the message
            // if that's the case. In addition, other parts of the following
            // code blocks rely on the DivideByZero flag being false to do
            // their jobs.
            c.DivideByZero = false;

            if (ModelState.IsValid)
            {
                // Handle each button/action separately.
                if (Request.Form["Divide"] != null)
                {
                    // Turn off the flag for repeating equals. If this flag is
                    // still enabled when we hit Equals again, math will not
                    // happen correctly.
                    c.DoEqualsRepeat = false;

                    // Record the last value as the currently provided number.
                    c.LastValue = m.CurrentValue;

                    if (!c.FirstOperation)
                    {
                        // If this is not the first operation that has been
                        // triggered, perform the actual math for the prior
                        // operation.
                        c.PerformCalculation();
                    }
                    else
                    {
                        // If this is the first operation that has been
                        // triggered, we do not want to do any math yet, we
                        // want to set up the running total and toggle the
                        // FirstOperation flag for the next operation.
                        c.RunningTotal = c.LastValue;
                        c.FirstOperation = false;
                    }

                    if (!c.DivideByZero)
                    {
                        // If we did not have a divide-by-zero error, or it's
                        // the first operation that's been triggered, set the
                        // current operation, set up the display for the
                        // current value, and add to the operation history.
                        c.CurrentOperation = Models.Calculator.Operations.Divide;
                        c.CurrentValue = c.RunningTotal;
                        c.CurrentHistory += c.LastValue + " / ";
                    }
                }
                else if (Request.Form["Multiply"] != null)
                {
                    c.DoEqualsRepeat = false;
                    c.LastValue = m.CurrentValue;

                    if (!c.FirstOperation)
                    {
                        c.PerformCalculation();
                    }
                    else
                    {
                        c.RunningTotal = c.LastValue;
                        c.FirstOperation = false;
                    }

                    if (!c.DivideByZero)
                    {
                        c.CurrentOperation = Models.Calculator.Operations.Multiply;
                        c.CurrentValue = c.RunningTotal;
                        c.CurrentHistory += c.LastValue + " * ";
                    }
                }
                else if (Request.Form["Plus"] != null)
                {
                    c.DoEqualsRepeat = false;
                    c.LastValue = m.CurrentValue;

                    if (!c.FirstOperation)
                    {
                        c.PerformCalculation();
                    }
                    else
                    {
                        c.RunningTotal = c.LastValue;
                        c.FirstOperation = false;
                    }

                    if (!c.DivideByZero)
                    {
                        c.CurrentOperation = Models.Calculator.Operations.Add;
                        c.CurrentValue = c.RunningTotal;
                        c.CurrentHistory += c.LastValue + " + ";
                    }
                }
                else if (Request.Form["Minus"] != null)
                {
                    c.DoEqualsRepeat = false;
                    c.LastValue = m.CurrentValue;

                    if (!c.FirstOperation)
                    {
                        c.PerformCalculation();
                    }
                    else
                    {
                        c.RunningTotal = c.LastValue;
                        c.FirstOperation = false;
                    }

                    if (!c.DivideByZero)
                    {
                        c.CurrentOperation = Models.Calculator.Operations.Subtract;
                        c.CurrentValue = c.RunningTotal;
                        c.CurrentHistory += c.LastValue + " - ";
                    }
                }
                else if (Request.Form["Reciprocate"] != null)
                {
                    c.DoEqualsRepeat = false;

                    if (0 == m.CurrentValue)
                    {
                        // If the user tried to reciprocate 0, tell him/her no.
                        c.CurrentValue = m.CurrentValue;
                        c.DivideByZero = true;
                    }
                    else
                    {
                        // We reciprocate the number in-place.
                        c.CurrentValue = 1 / m.CurrentValue;

                        // This is a bit buggy, disabled for now.
                        /*string[] split = c.CurrentHistory.Split(delims, StringSplitOptions.None);

                        if (0 == split[split.Length - 1].Length)
                        {
                            c.CurrentHistory += "reciproc(" + m.CurrentValue + ")";
                        }
                        else
                        {
                            c.CurrentHistory = c.CurrentHistory.Replace(split[split.Length - 1], "reciproc(" + split[split.Length - 1] + ")");
                        }*/
                    }
                }
                else if (Request.Form["Percentage"] != null)
                {
                    c.DoEqualsRepeat = false;

                    // The Windows calculator performs the percentage operator
                    // like such (we do this in-place):
                    c.CurrentValue = (c.RunningTotal / 100) * m.CurrentValue;
                }
                else if (Request.Form["SquareRoot"] != null)
                {
                    c.DoEqualsRepeat = false;

                    // We do the square root in-place.
                    c.CurrentValue = (double)Math.Sqrt((double)m.CurrentValue);
                }
                else if (Request.Form["Equals"] != null)
                {
                    double temp;

                    if (!c.DoEqualsRepeat)
                    {
                        // If this is not a repeat equals press, we'll perform
                        // the initial calculation as normal using the running
                        // total.
                        c.LastValue = m.CurrentValue;
                        temp = c.RunningTotal;
                    }
                    else
                    {
                        // If this is a repeat equals press, we want to perform
                        // the last mathematical operation again on the current
                        // value.
                        temp = m.CurrentValue;
                    }

                    // The following is a special modificiation of the function
                    // PerformCalculation in the Calculator Model.
                    switch (c.CurrentOperation)
                    {
                        case Models.Calculator.Operations.Add:
                            temp += c.LastValue;
                            break;

                        case Models.Calculator.Operations.Subtract:
                            temp -= c.LastValue;
                            break;

                        case Models.Calculator.Operations.Multiply:
                            temp *= c.LastValue;
                            break;

                        case Models.Calculator.Operations.Divide:
                            if (0 == c.LastValue)
                            {
                                c.DivideByZero = true;
                                break;
                            }

                            temp /= c.LastValue;
                            break;

                        case Models.Calculator.Operations.None:
                            temp = c.LastValue;
                            break;
                    }

                    if (!c.DivideByZero)
                    {
                        // If we did not divide by zero, clear the running
                        // total so we do not encounter problems later. Set the
                        // equals repeat flag in case the user hits Equals
                        // again. Wipe out the operation history and reset the
                        // FirstOperation flag, and return the resultant
                        // calculation back to the View.
                        c.RunningTotal = 0;
                        c.DoEqualsRepeat = true;
                        c.CurrentHistory = "";
                        c.FirstOperation = true;
                        c.CurrentValue = temp;
                    }
                }
                else if (Request.Form["MemoryClear"] != null)
                {
                    c.DoEqualsRepeat = false;

                    c.MemoryStore = 0;
                }
                else if (Request.Form["MemorySave"] != null)
                {
                    c.DoEqualsRepeat = false;

                    c.LastValue = m.CurrentValue;
                    c.MemoryStore = c.LastValue;
                    c.CurrentValue = m.CurrentValue;
                }
                else if (Request.Form["MemoryRecall"] != null)
                {
                    c.DoEqualsRepeat = false;

                    c.CurrentValue = c.MemoryStore;
                }
                else if (Request.Form["MemoryAdd"] != null)
                {
                    c.DoEqualsRepeat = false;

                    c.LastValue = m.CurrentValue;
                    c.MemoryStore += c.LastValue;
                    c.CurrentValue = m.CurrentValue;
                }
                else if (Request.Form["MemorySubtract"] != null)
                {
                    c.DoEqualsRepeat = false;

                    c.LastValue = m.CurrentValue;
                    c.MemoryStore -= c.LastValue;
                    c.CurrentValue = m.CurrentValue;
                }
                else if (Request.Form["Clear"] != null)
                {
                    // This is literally just a reset of the entire state of
                    // the calculator with the exception of the Memory storage.
                    c.DoEqualsRepeat = false;
                    c.FirstOperation = true;
                    c.DivideByZero = false;
                    c.CurrentValue = 0;
                    c.RunningTotal = 0;
                    c.LastValue = 0;
                    c.CurrentHistory = "";
                    c.CurrentOperation = Models.Calculator.Operations.None;
                }
            }

            ModelState.Clear();

            return View(c);
        }
    }
}
