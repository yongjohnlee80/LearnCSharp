using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    /// <summary>
    /// Type AdventDay8
    /// This is the unit testing entry.
    /// Various Solution methods are implmented to interact with the unit to be tested.
    /// This class also modifies the training data into proper data structure.
    /// </summary>
    internal class AdventDay8
    {
        // fields
        string[] data;
        // constructor
        public AdventDay8(string[] data)
        {
            this.data = data;
        }
        // methods.
        public int FindSolution()
        {
            var rules = new Processor();
            rules.LoadInstructions(data);
            return 0;
        }
    }

    /**************************************************************************
     * Implement classes on separate files once test is successfully tested.
     * Author: Yong Sung Lee.
     * Date: 2021-11-28.
     **************************************************************************/

    /// <summary>
    /// Custom Assembly Keywords and Flags
    /// This enum values should be exclusively used to interact with instruction
    /// sets and arguments.
    /// This allows future modification of the keyword and flag values without
    /// breaking codes.
    /// </summary>
    public enum AsmKey
    {
        // instruction set
        nop = 1,
        acc = 2,
        jmp = 4,

        // flags
        repeated = 256,
        overflow = 512
    };

    public class Instruction
    {
        protected long data; // instruction (BYTE:Flags + BYTE:Command + WORD:Argument)

        public long Data
        {
            get; set;
        }

        // Constructor
        public Instruction(AsmKey inst, int arg)
        {
            Load(inst, arg);
        }

        /// <summary>
        /// Load method loads instruction and arguement.
        /// Instruction field contains the both command and flag; however,
        /// bit operations are performed to avoid conflits.
        /// </summary>
        /// <param name="inst">instruction</param>
        /// <param name="arg">argument</param>
        /// <returns></returns>
        public Instruction Load(AsmKey inst, int arg)
        {
            this.data = (long)inst << 16 | (long)arg;
            return this;
        }

        /// <summary>
        /// Retrive method returns tuple of (command, argument).
        /// </summary>
        /// <returns></returns>
        public (AsmKey, int) Retrieve()
        {
            return ((AsmKey)(this.data >> 16), (int)(this.data & 0xFFFF));
        }
    }

    public static class InstructionExtensions
    {
        public static bool LoadFromString(this List<Instruction> input, string line)
        {
            try
            {
                line = line.Replace("+", "");
                string[] command = Regex.Split(line, @"\d+");
                Enum.TryParse(command[0], out AsmKey asmKey);
                string[] arg = Regex.Split(line, @"\D+");
                int number = Convert.ToInt32(arg[0]);
                input.Add(new Instruction(asmKey, number));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class Processor
    {
        // Registors.
        int acc = 0;

        // Memory (implemented as heap by .NET).
        List<Instruction> data = new List<Instruction>();

        public bool LoadInstructions(string[] textLines)
        {
            foreach(string line in textLines)
            {
                if(!data.LoadFromString(line))
                {
                    return false;
                }
            }
            return true;
        }
    }
}