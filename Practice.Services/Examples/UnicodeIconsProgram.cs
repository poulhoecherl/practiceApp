using System;
using System.Text;

namespace Practice.Services.Examples
{
    /// <summary>
    /// Simple console program to demonstrate Unicode icons
    /// </summary>
    public class UnicodeIconsProgram
    {
        public static void Main(string[] args)
        {
            // Set console encoding to UTF-8 for proper Unicode display
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("Unicode Icons Demo for C# Console Applications");
            Console.WriteLine("==============================================\n");
            
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Show all available icons");
                Console.WriteLine("2. Show menu example");
                Console.WriteLine("3. Show progress bar example");
                Console.WriteLine("4. Show usage example");
                Console.WriteLine("5. Quick icon reference");
                Console.WriteLine("0. Exit");
                Console.Write("\n→ Enter your choice: ");
                
                string input = Console.ReadLine();
                Console.Clear();
                
                switch (input)
                {
                    case "1":
                        UnicodeIconsDemo.DisplayAllIcons();
                        break;
                    case "2":
                        UnicodeIconsDemo.ShowMenuExample();
                        Console.ReadKey();
                        break;
                    case "3":
                        UnicodeIconsDemo.ShowProgressBarExample();
                        break;
                    case "4":
                        UnicodeIcons.ShowUsageExample();
                        break;
                    case "5":
                        ShowQuickReference();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye! ✓");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid option. Please try again.");
                        break;
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        private static void ShowQuickReference()
        {
            Console.WriteLine("QUICK ICON REFERENCE");
            Console.WriteLine("===================\n");
            
            Console.WriteLine("MOST COMMONLY USED ICONS:");
            Console.WriteLine($"✓ Success/Complete: \\u2713");
            Console.WriteLine($"✗ Error/Failed: \\u2717");
            Console.WriteLine($"⚠ Warning: \\u26A0");
            Console.WriteLine($"ℹ Information: \\u2139");
            Console.WriteLine($"→ Arrow Right: \\u2192");
            Console.WriteLine($"▶ Play/Start: \\u25B6");
            Console.WriteLine($"⏸ Pause: \\u23F8");
            Console.WriteLine($"⏹ Stop: \\u23F9");
            Console.WriteLine($"⚙ Settings/Gear: \\u2699");
            Console.WriteLine($"📊 Statistics/Chart: \\uD83D\\uDCCA");
            Console.WriteLine($"❓ Help/Question: \\u2753");
            Console.WriteLine($"❌ Close/Exit: \\u274C");
            Console.WriteLine();
            
            Console.WriteLine("PROGRESS INDICATORS:");
            Console.WriteLine($"█ Full block: \\u2588");
            Console.WriteLine($"▓ Dark shade: \\u2593");
            Console.WriteLine($"▒ Medium shade: \\u2592");
            Console.WriteLine($"░ Light shade: \\u2591");
            Console.WriteLine($"⠋ Spinner 1: \\u280B");
            Console.WriteLine($"⠙ Spinner 2: \\u2819");
            Console.WriteLine($"⠸ Spinner 3: \\u2838");
            Console.WriteLine();
            
            Console.WriteLine("BOX DRAWING (for menus/tables):");
            Console.WriteLine($"┌ Top-left: \\u250C");
            Console.WriteLine($"┐ Top-right: \\u2510");
            Console.WriteLine($"└ Bottom-left: \\u2514");
            Console.WriteLine($"┘ Bottom-right: \\u2518");
            Console.WriteLine($"─ Horizontal: \\u2500");
            Console.WriteLine($"│ Vertical: \\u2502");
            Console.WriteLine($"├ Left tee: \\u251C");
            Console.WriteLine($"┤ Right tee: \\u2524");
            Console.WriteLine();
            
            Console.WriteLine("Example usage in C#:");
            Console.WriteLine("Console.WriteLine(\"\\u2713 Task completed\");");
            Console.WriteLine("Console.WriteLine(\"\\u26A0 Warning message\");");
            Console.WriteLine("Console.WriteLine(\"\\u2192 Next step\");");
        }
    }
}
