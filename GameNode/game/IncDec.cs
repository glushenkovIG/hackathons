using System;
using Com.Expload;

[Program]
class IncDec {

    public Mapping<String, int> WinCount = new Mapping<String, int>();
    public Mapping<String, int> LoseCount = new Mapping<String, int>();


    public void Fight(String a, String b) {
	    WinCount.put(a, WinCount.getDefault(a, 0) + 1);
	    LoseCount.put(b, LoseCount.getDefault(b, 0) + 1);
    }

    public int ShowWins(String a)
    {   

        return WinCount.getDefault(a, 0);
    }

    public int ShowLoses(String a)
    {
        return LoseCount.getDefault(a, 0);
    }

}
class MainClass { public static void Main() {} }
