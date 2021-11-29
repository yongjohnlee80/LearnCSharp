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

        public int FindSolution2()
        {
            var gameBoy = new Processor();
            gameBoy.LoadInstructions(data);
            return gameBoy.Debug();
        }
    }

    /**************************************************************************
     * Implement classes on separate files once test is successfully tested.
     * Author: Yong Sung Lee.
     * Date: 2021-11-28.
     **************************************************************************/

    /// <summary>
    /// Custom Assembly Keywords.
    /// </summary>
    public enum AsmKey
    {
        // instruction set
        nop = 1,
        acc = 2,
        jmp = 4,
        
        mask = 5
    };

    public class Instruction
    {
        AsmKey key = AsmKey.nop;
        int arg = 0;
        bool processed = false;

        // Constructor
        public Instruction(AsmKey inst, int arg)
        {
            Load(inst, arg);
        }

        public Instruction Load(AsmKey inst, int arg)
        {
            key = inst;
            this.arg = arg;
            return this;
        }

        /// <summary>
        /// Retrive method returns tuple of (command, argument).
        /// </summary>
        /// <returns></returns>
        public (AsmKey, int) Retrieve(bool setProcessedFlag = true)
        {
            if(setProcessedFlag) processed = true;
            return (key, arg);
        }

        public bool FlagProcessed()
        {
            return processed;
        }

        public bool IsJmpNop()
        {
            return (key == AsmKey.nop || key == AsmKey.jmp);
        }

        public void Flip()
        {
            key = key ^ AsmKey.mask;
        }

        public void ResetFlag()
        {
            processed = false;
        }
    }

    public static class InstructionExtensions
    {
        public static bool LoadFromString(this List<Instruction> input, string line)
        {
            try
            {
                line = line.Replace("+", "");
                string[] command = line.Split(' ');
                Enum.TryParse(command[0], out AsmKey asmKey);
                int number = Convert.ToInt32(command[1]);
                input.Add(new Instruction(asmKey, number));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void ResetFlags(this List<Instruction> input)
        {
            foreach(Instruction i in input)
            {
                i.ResetFlag();
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
            StringBuilder log = new StringBuilder();
            foreach(Instruction i in data)
            {
                log.Append($"{i.Retrieve(false)}\n");
            }
            File.WriteAllText("log.txt", log.ToString());
            return true;
        }

        public int Run(bool debug = false, bool AllowRepetition = false)
        {
            int insPtr = 0; // Instruction Pointer.
            AsmKey key;
            int arg = 0;
            while(insPtr < data.Count)
            {
                if (!AllowRepetition && data[insPtr].FlagProcessed())
                {
                    if (debug) return (insPtr * -1);
                    else return acc;
                }
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
                                    insPtr++;
                                    break;
                }
            }
            return acc;
        }

        public int Debug()
        {
            int result = Run(true);
            if (result > 0) return result;
            int pos = 0;
            do {
                Modify(pos);
                acc = 0;
                data.ResetFlags();
                result = Run(true);
                Modify(pos++);
            } while (result <= 0 && pos < data.Count);
            return result;
        }

        public void Modify(int pos)
        {
            data[pos].Flip();
        }
    }
}