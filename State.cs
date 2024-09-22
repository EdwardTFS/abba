using System.Collections;

namespace Abba;
class State{
    public State(BitArray pattern, int delta)
    {
        this.Pattern = pattern;
        this.Delta = delta;
        
    }
    public BitArray Pattern { get; set; }
    public int Delta { get; set; }
}