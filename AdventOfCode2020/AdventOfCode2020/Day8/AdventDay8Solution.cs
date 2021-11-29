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
            var gameBoy = new Processor();
            gameBoy.LoadInstructions(data);
            return gameBoy.Run();
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
        processed = 256,
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
            this.data = ((long)inst) << 32 | ((long)arg);
            return this;
        }

        /// <summary>
        /// Retrive method returns tuple of (command, argument).
        /// </summary>
        /// <returns></returns>
        public (AsmKey, int) Retrieve()
        {
            this.data = this.data | (int)AsmKey.processed;
            return ((AsmKey)(this.data >> 32), (int)(this.data & 0x0000FFFF));
        }

        public bool FlagProcessed()
        {
            return (this.data & (int)AsmKey.processed) != 0;
        }
    }

    public static class InstructionExtensions
    {
        public static bool LoadFromString(this List<Instruction> input, string line)
        {
            //try
            //{
                line = line.Replace("+", "");
                string[] command = line.Split(' ');
                Enum.TryParse(command[0], out AsmKey asmKey);
                int number = Convert.ToInt32(command[1]);
                input.Add(new Instruction(asmKey, number));
                return true;


                //line = line.Replace("+", "");
                //string[] command = Regex.Split(line, @"\d+");
                //Enum.TryParse(command[0], out AsmKey asmKey);
                //string[] arg = Regex.Split(line, @"\D+");
                //int number = Convert.ToInt32(arg[1]);
                //input.Add(new Instruction(asmKey, number));
                //return true;
            //}
            //catch
            //{
            //    return false;
            //}
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
            StringBuilder log = new StringBuilder();
            foreach(Instruction i in data)
            {
                log.Append($"{i.Retrieve()}\n");
            }
            File.WriteAllText("log.txt", log.ToString());
            return true;
        }

        public int Run(bool AllowRepetition = false)
        {
            int insPtr = 0; // Instruction Pointer.
            AsmKey key = new AsmKey();
            int arg = 0;
            while(insPtr < data.Count)
            {
                if (!AllowRepetition && data[insPtr].FlagProcessed()) break;
                (key, arg) = data[insPtr].Retrieve();
                switch(key)
                {
                    case AsmKey.nop: 
                                    insPtr++;
                                    break;
                    case AsmKey.acc: 
                                    acc += arg;
                                    insPtr++;
                                    break;
                    case AsmKey.jmp: 
                                    insPtr += arg;
                                    break;
                    default:
                                    break;
                }
            }
            return insPtr;
        }
    }
}