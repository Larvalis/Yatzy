using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahtzeeLibrary
{
    public class Yahtzee
    {
        Random random = new Random();

        int[] dies = new int[5];
        bool[] hold = new bool[5];
        int[] stat = new int[7];

        public void Roll(bool hold0, bool hold1, bool hold2, bool hold3, bool hold4)
        {
            hold[0] = hold0; hold[1] = hold1; hold[2] = hold2; hold[3] = hold3; hold[4] = hold4; 

            for (int i = 0; i < 5; i++)
            {
                if (!hold[i])
                    dies[i] = random.Next(1, 7);
            }
            initStat();
        }

        public int[] Dies
        {
            get { return (int[])dies.Clone(); }
        }

        // initializes stat from dies
        private void initStat()
        {
            // reset stat
            for (int i = 1; i <= 6; i++)
                stat[i] = 0;

            // calculate stat from dies
            foreach (int n in dies)
                stat[n]++;
        }

        // returns sum for dies showing faceValue
        public int valueSpecificFace(int faceValue)
        {
            int count = 0;
            foreach (int n in dies)
                if (n == faceValue) count++;

            return count * faceValue;
        }

        // returns value for dies showing count dies with same faceValue
        // valueSameOdAKind(3) returns the value for 3 of-a-kind
        // valueSameOdAKind(4) returns the value for 4 of-a-kind
        public int valueSameOfAKind(int count)
        {
            int value = 0;
            for (int i = 1; i <= 6; i++)
                if (stat[i] >= count) value = count * i;

            return value;
        }

        // returns the value of yatzy
        public int valueYatzy()
        {
            if (valueSameOfAKind(5) > 0)
                return 50;
            else
                return 0;
        }

        //returns the value of Chance
        public int valueChance()
        {
            int sum = 0;
            foreach(int n in dies)
              sum+=n;

            return sum;
        }

        // returns value for one pair
        public int valueOnePair()
        {
            if (valueSpecificFace(6) >= 12)
                return 12;
            else if (valueSpecificFace(5) >= 10)
                return 10;
            else if (valueSpecificFace(4) >= 8)
                return 8;
            else if (valueSpecificFace(3) >= 6)
                return 6;
            else if (valueSpecificFace(2) >= 4)
                return 4;
            else if (valueSpecificFace(1) >= 2)
                return 2;
            else  
                return 0;
        }

        // returns value of TwoPair
        public int valueTwoPair()
        {
            int firstPair = valueOnePair();
            if (firstPair > 10 && valueSpecificFace(5) >= 10)
                return 10 + firstPair;
            else if (firstPair > 8 && valueSpecificFace(4) >= 8)
                return 8 + firstPair;
            else if (firstPair > 6 && valueSpecificFace(3) >= 6)
                return 6 + firstPair;
            else if (firstPair > 4 && valueSpecificFace(2) >= 4)
                return 4 + firstPair;
            else if (firstPair > 2 && valueSpecificFace(1) >= 2)
                return 2 + firstPair;
            else
                return 0;
        }

        // returns the value of SmallStraight
        public int valueSmallStraight()
        {
            int count, countDies = 0;
            for (int i = 1; i <= 5; i++)
            {
                count = 0;
                foreach (int n in dies)
                    if (n == i) count++;
                if (count > 0) countDies++;
            }
            if (countDies == 5)
                return 15;
            return 0;
        }

        // returns the value of LargeStraight
        public int valueLargeStraight()
        {
            int count, countDies = 0;
            for (int i = 2; i <= 6; i++)
            {
                count = 0;
                foreach (int n in dies)
                    if (n == i) count++;
                if (count > 0) countDies++;
            }
            if (countDies == 5)
                return 20;
            return 0;
        }

        // returns the value of FullHouse
        public int valueFullHouse()
        {
            int firstThree = valueOf3OfAKind();
            if (firstThree != 18 && firstThree != 0 && valueSpecificFace(6) >= 12)
                return 12 + firstThree;
            else if (firstThree != 15 && firstThree != 0 && valueSpecificFace(5) >= 10)
                return 10 + firstThree;
            else if (firstThree != 12 && firstThree != 0 && valueSpecificFace(4) >= 8)
                return 8 + firstThree;
            else if (firstThree != 9 && firstThree != 0 && valueSpecificFace(3) >= 6)
                return 6 + firstThree;
            else if (firstThree != 6 && firstThree != 0 && valueSpecificFace(2) >= 4)
                return 4 + firstThree;
            else if (firstThree != 3 && firstThree != 0 && valueSpecificFace(1) >= 2)
                return 2 + firstThree;
            else
                return 0;
        }

        // help function to full house
        int valueOf3OfAKind()
        {
            if (valueSpecificFace(6) >= 18)
                return 18;
            else if (valueSpecificFace(5) >= 15)
                return 15;
            else if (valueSpecificFace(4) >= 12)
                return 12;
            else if (valueSpecificFace(3) >= 9)
                return 9;
            else if (valueSpecificFace(2) >= 6)
                return 6;
            else if (valueSpecificFace(1) >= 3)
                return 3;
            else
                return 0;
        }
    }
}
