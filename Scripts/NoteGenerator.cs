using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteGenerator : MonoBehaviour
{
	public int LineLength;
	public int NumberOfLines;
	public float ChanceOfPlay;
	public class Line{
		public List<int> Notes;
		public List<bool> BoolList;
		public Line(int size){
			Notes = new List<int>();
			BoolList = new List<bool>();
		}
	}
	public List<Scale> Scales;
	private List<Line> Lines = new List<Line>();
	public bool NextLine{
		get;set;
	}
	private int noteIterator;
	private int lineIterator;
	    
	void Awake(){
		Generate();
	}
	public void Generate(){
		int random = NumberOfLines;
		
		Lines = new List<Line>();
		//Debug.Log("generatin");
		for(int i =0;i<=random;i++){
			float localChance = ChanceOfPlay;
			Line newLine = new Line(LineLength);
			for(int x=0;x<=LineLength-1;x++){
				int note = Scales[CurrentScale].pitches[UnityEngine.Random.Range(0,Scales[CurrentScale].pitches.Count)];
				newLine.Notes.Add(note);
				bool Boolean  = (Random.value > localChance);
				if(Boolean){localChance+=(Random.value+1);}
				else{localChance+=(Random.value-1);}
				newLine.BoolList.Add(Boolean);
			}
			Lines.Add(newLine);
		}
	}
	
	public IntEvent OutputNote;
	public UnityEvent OnLineEnd;
	public OutputBoolArray.BoolArrayEvent BoolArrayEvent;
	private int CurrentNote;
	public int CurrentScale;
	
	public void Output(){
		//Debug.Log("output",gameObject);	
		if (++noteIterator > Lines[lineIterator].Notes.Count-1){
			noteIterator = 0;
			OnLineEnd.Invoke();
			if(NextLine){
				if(++lineIterator >= Lines.Count) lineIterator = 0;
				
				NextLine = false;
			}
			
		}
	                        
		//Debug.Log(noteIterator + " " + lineIterator);
		CurrentNote = Lines[lineIterator].Notes[noteIterator];
		OutputNote.Invoke(CurrentNote);
	}
	
	public void OutputBoolArray(){
		//Debug.Log("outputting");
		bool[] output = Lines[lineIterator].BoolList.ToArray();
		BoolArrayEvent.Invoke(output);
	}
}
