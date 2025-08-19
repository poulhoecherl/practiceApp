using System;
using System.Text;

namespace Practice.Services.Examples
{
    /// <summary>
    /// Demonstration of UTF-16 Unicode icons that can be used in C# console applications
    /// </summary>
    public class UnicodeIconsDemo
    {
        public static void DisplayAllIcons()
        {
            // Set console to support UTF-8 encoding for better Unicode support
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("=== UTF-16 Unicode Icons for C# Console Applications ===\n");

            // Basic Symbols
            Console.WriteLine("🔹 BASIC SYMBOLS:");
            Console.WriteLine($"  ✓ Checkmark: \\u2713 - '{'\u2713'}'");
            Console.WriteLine($"  ✗ X Mark: \\u2717 - '{'\u2717'}'");
            Console.WriteLine($"  ★ Star: \\u2605 - '{'\u2605'}'");
            Console.WriteLine($"  ☆ Empty Star: \\u2606 - '{'\u2606'}'");
            Console.WriteLine($"  ● Bullet: \\u25CF - '{'\u25CF'}'");
            Console.WriteLine($"  ○ Empty Bullet: \\u25CB - '{'\u25CB'}'");
            Console.WriteLine($"  ◆ Diamond: \\u25C6 - '{'\u25C6'}'");
            Console.WriteLine($"  ◇ Empty Diamond: \\u25C7 - '{'\u25C7'}'");
            Console.WriteLine($"  ■ Square: \\u25A0 - '{'\u25A0'}'");
            Console.WriteLine($"  □ Empty Square: \\u25A1 - '{'\u25A1'}'");
            Console.WriteLine();

            // Arrows
            Console.WriteLine("🔹 ARROWS:");
            Console.WriteLine($"  → Right Arrow: \\u2192 - '{'\u2192'}'");
            Console.WriteLine($"  ← Left Arrow: \\u2190 - '{'\u2190'}'");
            Console.WriteLine($"  ↑ Up Arrow: \\u2191 - '{'\u2191'}'");
            Console.WriteLine($"  ↓ Down Arrow: \\u2193 - '{'\u2193'}'");
            Console.WriteLine($"  ⇒ Double Right: \\u21D2 - '{'\u21D2'}'");
            Console.WriteLine($"  ⇐ Double Left: \\u21D0 - '{'\u21D0'}'");
            Console.WriteLine($"  ➤ Triangle Right: \\u27A4 - '{'\u27A4'}'");
            Console.WriteLine($"  ▶ Play: \\u25B6 - '{'\u25B6'}'");
            Console.WriteLine($"  ◀ Back: \\u25C0 - '{'\u25C0'}'");
            Console.WriteLine();

            // Status Icons
            Console.WriteLine("🔹 STATUS ICONS:");
            Console.WriteLine($"  ⚠ Warning: \\u26A0 - '{'\u26A0'}'");
            Console.WriteLine($"  ⚡ Lightning: \\u26A1 - '{'\u26A1'}'");
            Console.WriteLine($"  ✉ Envelope: \\u2709 - '{'\u2709'}'");
            Console.WriteLine($"  ☎ Phone: \\u260E - '{'\u260E'}'");
            Console.WriteLine($"  ⚙ Gear: \\u2699 - '{'\u2699'}'");
            Console.WriteLine($"  ⛔ No Entry: \\u26D4 - '{'\u26D4'}'");
            Console.WriteLine($"  ✅ Check Mark Button: \\u2705 - '{'\u2705'}'");
            Console.WriteLine($"  ❌ Cross Mark: \\u274C - '{'\u274C'}'");
            Console.WriteLine($"  ❗ Exclamation: \\u2757 - '{'\u2757'}'");
            Console.WriteLine($"  ❓ Question: \\u2753 - '{'\u2753'}'");
            Console.WriteLine();

            // Math and Technical
            Console.WriteLine("🔹 MATH & TECHNICAL:");
            Console.WriteLine($"  ± Plus Minus: \\u00B1 - '{'\u00B1'}'");
            Console.WriteLine($"  × Multiplication: \\u00D7 - '{'\u00D7'}'");
            Console.WriteLine($"  ÷ Division: \\u00F7 - '{'\u00F7'}'");
            Console.WriteLine($"  ≈ Almost Equal: \\u2248 - '{'\u2248'}'");
            Console.WriteLine($"  ≠ Not Equal: \\u2260 - '{'\u2260'}'");
            Console.WriteLine($"  ≤ Less Equal: \\u2264 - '{'\u2264'}'");
            Console.WriteLine($"  ≥ Greater Equal: \\u2265 - '{'\u2265'}'");
            Console.WriteLine($"  ∞ Infinity: \\u221E - '{'\u221E'}'");
            Console.WriteLine($"  π Pi: \\u03C0 - '{'\u03C0'}'");
            Console.WriteLine($"  Σ Sigma: \\u03A3 - '{'\u03A3'}'");
            Console.WriteLine();

            // Box Drawing Characters
            Console.WriteLine("🔹 BOX DRAWING:");
            Console.WriteLine($"  ┌ Top Left: \\u250C - '{'\u250C'}'");
            Console.WriteLine($"  ┐ Top Right: \\u2510 - '{'\u2510'}'");
            Console.WriteLine($"  └ Bottom Left: \\u2514 - '{'\u2514'}'");
            Console.WriteLine($"  ┘ Bottom Right: \\u2518 - '{'\u2518'}'");
            Console.WriteLine($"  ─ Horizontal: \\u2500 - '{'\u2500'}'");
            Console.WriteLine($"  │ Vertical: \\u2502 - '{'\u2502'}'");
            Console.WriteLine($"  ├ Left Tee: \\u251C - '{'\u251C'}'");
            Console.WriteLine($"  ┤ Right Tee: \\u2524 - '{'\u2524'}'");
            Console.WriteLine($"  ┬ Top Tee: \\u252C - '{'\u252C'}'");
            Console.WriteLine($"  ┴ Bottom Tee: \\u2534 - '{'\u2534'}'");
            Console.WriteLine($"  ┼ Cross: \\u253C - '{'\u253C'}'");
            Console.WriteLine();

            // Progress and Loading
            Console.WriteLine("🔹 PROGRESS & LOADING:");
            Console.WriteLine($"  ▓ Dark Shade: \\u2593 - '{'\u2593'}'");
            Console.WriteLine($"  ▒ Medium Shade: \\u2592 - '{'\u2592'}'");
            Console.WriteLine($"  ░ Light Shade: \\u2591 - '{'\u2591'}'");
            Console.WriteLine($"  █ Full Block: \\u2588 - '{'\u2588'}'");
            Console.WriteLine($"  ▌ Left Half: \\u258C - '{'\u258C'}'");
            Console.WriteLine($"  ▐ Right Half: \\u2590 - '{'\u2590'}'");
            Console.WriteLine($"  ▀ Upper Half: \\u2580 - '{'\u2580'}'");
            Console.WriteLine($"  ▄ Lower Half: \\u2584 - '{'\u2584'}'");
            Console.WriteLine();

            // Music and Media
            Console.WriteLine("🔹 MUSIC & MEDIA:");
            Console.WriteLine($"  ♪ Musical Note: \\u266A - '{'\u266A'}'");
            Console.WriteLine($"  ♫ Beamed Notes: \\u266B - '{'\u266B'}'");
            Console.WriteLine($"  ♬ Beamed 16th: \\u266C - '{'\u266C'}'");
            Console.WriteLine($"  ♭ Flat Sign: \\u266D - '{'\u266D'}'");
            Console.WriteLine($"  ♯ Sharp Sign: \\u266F - '{'\u266F'}'");
            Console.WriteLine();

            // Weather and Nature
            Console.WriteLine("🔹 WEATHER & NATURE:");
            Console.WriteLine($"  ☀ Sun: \\u2600 - '{'\u2600'}'");
            Console.WriteLine($"  ☁ Cloud: \\u2601 - '{'\u2601'}'");
            Console.WriteLine($"  ☂ Umbrella: \\u2602 - '{'\u2602'}'");
            Console.WriteLine($"  ❄ Snowflake: \\u2744 - '{'\u2744'}'");
            Console.WriteLine($"  ⚡ Lightning: \\u26A1 - '{'\u26A1'}'");
            Console.WriteLine($"  ☽ Moon: \\u263D - '{'\u263D'}'");
            Console.WriteLine();

            // Hearts and Emotions
            Console.WriteLine("🔹 HEARTS & EMOTIONS:");
            Console.WriteLine($"  ♥ Heart: \\u2665 - '{'\u2665'}'");
            Console.WriteLine($"  ♡ Empty Heart: \\u2661 - '{'\u2661'}'");
            Console.WriteLine($"  ☺ Smiling: \\u263A - '{'\u263A'}'");
            Console.WriteLine($"  ☹ Frowning: \\u2639 - '{'\u2639'}'");
            Console.WriteLine();

            // Currency
            Console.WriteLine("🔹 CURRENCY:");
            Console.WriteLine($"  $ Dollar: \\u0024 - '$'");
            Console.WriteLine($"  € Euro: \\u20AC - '{'\u20AC'}'");
            Console.WriteLine($"  £ Pound: \\u00A3 - '{'\u00A3'}'");
            Console.WriteLine($"  ¥ Yen: \\u00A5 - '{'\u00A5'}'");
            Console.WriteLine($"  ¢ Cent: \\u00A2 - '{'\u00A2'}'");
            Console.WriteLine();

            // Miscellaneous Useful Icons
            Console.WriteLine("🔹 MISCELLANEOUS:");
            Console.WriteLine($"  © Copyright: \\u00A9 - '{'\u00A9'}'");
            Console.WriteLine($"  ® Registered: \\u00AE - '{'\u00AE'}'");
            Console.WriteLine($"  ™ Trademark: \\u2122 - '{'\u2122'}'");
            Console.WriteLine($"  § Section: \\u00A7 - '{'\u00A7'}'");
            Console.WriteLine($"  ¶ Paragraph: \\u00B6 - '{'\u00B6'}'");
            Console.WriteLine($"  † Dagger: \\u2020 - '{'\u2020'}'");
            Console.WriteLine($"  ‡ Double Dagger: \\u2021 - '{'\u2021'}'");
            Console.WriteLine($"  • Bullet: \\u2022 - '{'\u2022'}'");
            Console.WriteLine($"  … Ellipsis: \\u2026 - '{'\u2026'}'");
            Console.WriteLine($"  ‰ Per Mille: \\u2030 - '{'\u2030'}'");
            Console.WriteLine();
        }

        /// <summary>
        /// Example usage of icons in a console menu
        /// </summary>
        public static void ShowMenuExample()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("┌─────────────────────────────────┐");
            Console.WriteLine("│        ⚙ MAIN MENU ⚙          │");
            Console.WriteLine("├─────────────────────────────────┤");
            Console.WriteLine("│                                 │");
            Console.WriteLine("│  1. ▶ Start Session            │");
            Console.WriteLine("│  2. ⏹ Stop Session             │");
            Console.WriteLine("│  3. ⚙ Settings                 │");
            Console.WriteLine("│  4. 📊 Statistics              │");
            Console.WriteLine("│  5. ❓ Help                     │");
            Console.WriteLine("│  6. ❌ Exit                     │");
            Console.WriteLine("│                                 │");
            Console.WriteLine("└─────────────────────────────────┘");
            Console.WriteLine();
            Console.Write("→ Select option: ");
        }

        /// <summary>
        /// Example of a progress bar using Unicode characters
        /// </summary>
        public static void ShowProgressBarExample()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("Progress Examples:");
            Console.WriteLine();
            
            // Simple progress bar
            for (int i = 0; i <= 20; i++)
            {
                Console.Write("\r[");
                Console.Write(new string('█', i));
                Console.Write(new string('░', 20 - i));
                Console.Write($"] {i * 5}%");
                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine("\n✓ Complete!");
            Console.WriteLine();
            
            // Spinning loader
            string[] spinners = { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
            Console.Write("Loading ");
            for (int i = 0; i < 30; i++)
            {
                Console.Write($"\r{spinners[i % spinners.Length]} Loading...");
                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine("\r✓ Loaded!    ");
        }
    }

    /// <summary>
    /// Static class containing commonly used Unicode icons as constants
    /// </summary>
    public static class UnicodeIcons
    {
        // Basic symbols
        public const string Checkmark = "\u2713";
        public const string XMark = "\u2717";
        public const string Star = "\u2605";
        public const string EmptyStar = "\u2606";
        public const string Bullet = "\u25CF";
        public const string EmptyBullet = "\u25CB";
        
        // Arrows
        public const string RightArrow = "\u2192";
        public const string LeftArrow = "\u2190";
        public const string UpArrow = "\u2191";
        public const string DownArrow = "\u2193";
        public const string Play = "\u25B6";
        public const string Back = "\u25C0";
        
        // Status
        public const string Warning = "\u26A0";
        public const string Lightning = "\u26A1";
        public const string Gear = "\u2699";
        public const string NoEntry = "\u26D4";
        public const string CheckButton = "\u2705";
        public const string CrossMark = "\u274C";
        public const string Exclamation = "\u2757";
        public const string Question = "\u2753";
        
        // Box drawing
        public const string TopLeft = "\u250C";
        public const string TopRight = "\u2510";
        public const string BottomLeft = "\u2514";
        public const string BottomRight = "\u2518";
        public const string Horizontal = "\u2500";
        public const string Vertical = "\u2502";
        
        // Progress
        public const string FullBlock = "\u2588";
        public const string LightShade = "\u2591";
        public const string MediumShade = "\u2592";
        public const string DarkShade = "\u2593";
        
        // Usage example
        public static void ShowUsageExample()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine($"{Checkmark} Task completed successfully");
            Console.WriteLine($"{XMark} Task failed");
            Console.WriteLine($"{Warning} Warning: Low disk space");
            Console.WriteLine($"{Lightning} Fast operation");
            Console.WriteLine($"{Gear} Processing...");
            Console.WriteLine();
            
            // Create a simple box
            Console.WriteLine($"{TopLeft}{new string(Horizontal[0], 20)}{TopRight}");
            Console.WriteLine($"{Vertical} Status: Online      {Vertical}");
            Console.WriteLine($"{BottomLeft}{new string(Horizontal[0], 20)}{BottomRight}");
        }
    }
}
