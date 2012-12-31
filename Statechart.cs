using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActionLogger
{
    
    public delegate void EnterPlaceEventHandler(Place sender);
    public delegate void LeavePlaceEventHandler(Place sender);
    public delegate void EnterTransitionEventHandler(Transition sender, Place from);
    public delegate void LeaveTransitionEventHandler(Place sender, Place to);

    
    public static class PNSettings
    {
        public const int TokenColors = 3;
        public const int MaxTokens = 3;
        public const int MaxTransitionsInComplexTransition = 5;
    }


    public enum TransitionStates : int
    {
        TRANSITION_NOTSIGNALED,        
        TRANSITION_SIGNALED,
    }

    public enum PlaceStates : int
    {
        PLACE_FREE,
        PLACE_FULL
        
    }
    public class Token 
    {

        int Color;
        object Host;
        public int CurrTokenIndex;
        public int CurrTokenColor;
        public void DestroyToken()
         {
         
         }
        
    }

    public struct Transition
    {
        public Place PlaceIn;
        public int[,] Tokens;
        public Place PlaceTo;
    }

    public class ComplexTransition
    {
        List<Transition> AtomicTransitions;
    }

    public class Place
    {
        public int[,] TokenCapacity;
        public List<Token> TokenList;
        public Place(int Capacity)
        {

        }
    }

    public class Statechart
    {
        /// <summary>
        /// PlacesTable
        /// </summary>
        public Place[] Places = new Place[3];
        public Transition[] Transitions = new Transition[3];

        
        public Statechart(int PlacesCount=3, int TransitionCount = 3)
        {
            if (PlacesCount != 3)
                Places = new Place[PlacesCount];
            if (TransitionCount != 3)
                Transitions = new Transition[TransitionCount];
        }

        public void AddTransition(Transition trans)
        {

        }

        public void AddPlace(int Capacity)
        {
            Places[0] = new Place(Capacity);
            
        }

        public void SetPlaceColorCap(int TokenColor, int Capacity)
        {

        }

        public void PlaceToken(object NewHost, Token token)
        {
            if (NewHost is Place)
            {
                ((Place)NewHost).TokenList.Add(token);
                
            }
            else
            if (NewHost is Transition)
            {
               
   
            }
            
        }

        public TransitionStates ComputeComplexTransition()
        {
            return TransitionStates.TRANSITION_SIGNALED;
        }

        public TransitionStates ComputeTransition()
        {
            return TransitionStates.TRANSITION_SIGNALED;
        }

        public PlaceStates ComputePlace()
        {
            return PlaceStates.PLACE_FREE;
        }

    }
}
