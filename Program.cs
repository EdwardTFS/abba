using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Abba;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ABBA detector!");
        BitArray ba = new BitArray(new bool[]{false,true,true,false});
        var currentState = new State(ba,4);
        List<State> thisLevelStates = new List<State>(){currentState};
        List<State> nextLevelStates = new List<State>(){};
        for(int level = 4; level < 56+1; level++){
            Console.WriteLine("Level {0} - count {1}",level,thisLevelStates.Count);
            bool currentBit = AbbaGenerator(level);
            List<BitArray> levelResults = new ();

            foreach(var s in thisLevelStates)
                nextLevelStates.AddRange(processState(s,currentBit,levelResults));
            if(levelResults.Count > 0){
                Console.WriteLine("Result# {0}",levelResults.Count);
                // foreach(var g in levelResults.GroupBy(r => r).OrderBy(g => g.Count())){
                //     Console.WriteLine("{0} * {1}={2}",g.Count(), g.Key.ToHexString(), g.Key.ToBitString());
                //}
            }
            //deduplicate
            thisLevelStates =  nextLevelStates.GroupBy(s1 => s1.Pattern.ToBitString()).Select(g => g.First()).ToList();
;
            nextLevelStates = new List<State>();

        }
        
    }

    private static IEnumerable<State> processState(State s, bool currentBit, List<BitArray> levelResults)
    {
        if(s.Delta == 0){
            levelResults.Add(s.Pattern);
            yield break;
        }
        bool backBit = s.Pattern[s.Pattern.Length - s.Delta];
        if( backBit == currentBit)
            yield return new State(s.Pattern,s.Delta-1);
        yield return new State(s.Pattern.Append(currentBit),s.Delta+1);
    }

    static bool AbbaGenerator(int elementNumber)
        => (elementNumber % 4) switch {
            0 => false,
            1 => true,
            2 => true,
            3 => false,
            _ => false // nigdy nie wystąpi
        };

    
}