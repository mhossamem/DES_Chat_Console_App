using System;
using System.Collections;

namespace DES_FInal
{
    class DES
    {
        static bool flag = false;
        static int[] Text_After_Round = new int[64];
        static int[] PermChoice1 = new int[56] {57, 49,  41, 33,  25,  17,  9,
                                 1, 58,  50, 42,  34,  26, 18,
                                10,  2,  59, 51,  43,  35, 27,
                                19, 11,   3, 60,  52,  44, 36,
                                63, 55,  47, 39,  31,  23, 15,
                                 7, 62,  54, 46,  38,  30, 22,
                                14,  6,  61, 53,  45,  37, 29,
                                21, 13,   5, 28,  20,  12,  4}; // Permutation and s-box tables
        static int[] PermChoice2 = new int[48] {14, 17, 11, 24,  1,  5,
                                 3, 28, 15,  6, 21, 10,
                                23, 19, 12,  4, 26,  8,
                                16,  7, 27, 20, 13,  2,
                                41, 52, 31, 37, 47, 55,
                                30, 40, 51, 45, 33, 48,
                                44, 49, 39, 56, 34, 53,
                                46, 42, 50, 36, 29, 32};
        static int[] ExpansionPerm = new int[48] {32,  1,  2,  3,  4,  5,
                             4,  5,  6,  7,  8,  9,
                             8,  9, 10, 11, 12, 13,
                            12, 13, 14, 15, 16, 17,
                            16, 17, 18, 19, 20, 21,
                            20, 21, 22, 23, 24, 25,
                            24, 25, 26, 27, 28, 29,
                            28, 29, 30, 31, 32,  1 };
        static int[] InitialMsgPerm = new int[64] {58, 50, 42, 34, 26, 18, 10, 2,
                                        60, 52, 44, 36, 28, 20, 12, 4,
                                        62, 54, 46, 38, 30, 22, 14, 6,
                                        64, 56, 48, 40, 32, 24, 16, 8,
                                        57, 49, 41, 33, 25, 17,  9, 1,
                                        59, 51, 43, 35, 27, 19, 11, 3,
                                        61, 53, 45, 37, 29, 21, 13, 5,
                                        63, 55, 47, 39, 31, 23, 15, 7 };
        static int[] final_message_permutation = new int[64] {40,  8, 48, 16, 56, 24, 64, 32,
                                                            39,  7, 47, 15, 55, 23, 63, 31,
                                                            38,  6, 46, 14, 54, 22, 62, 30,
                                                            37,  5, 45, 13, 53, 21, 61, 29,
                                                            36,  4, 44, 12, 52, 20, 60, 28,
                                                            35,  3, 43, 11, 51, 19, 59, 27,
                                                            34,  2, 42, 10, 50, 18, 58, 26,
                                                            33,  1, 41,  9, 49, 17, 57, 25};
        static int[] RightPerm = new int[32]  {16,  7, 20, 21,
                                    29, 12, 28, 17,
                                     1, 15, 23, 26,
                                     5, 18, 31, 10,
                                     2,  8, 24, 14,
                                    32, 27,  3,  9,
                                    19, 13, 30,  6,
                                    22, 11,  4, 25};
        static int[,] S1 = new int[4, 16] { {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
             { 0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8 },
            { 4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0 },
            { 15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13 }};
        static int[,] S2 = new int[4, 16] {{15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10 },
             { 3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5 },
             { 0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15},
            { 13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9 } };
        static int[,] S3 = new int[4, 16] {{10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8 },
           { 13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1},
            { 13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7 },
            { 1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12 }};
        static int[,] S4 = new int[4, 16] {{ 7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15 },
          {  13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9},
          {  10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4 },
            { 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14}};
        static int[,] S5 = new int[4, 16] {{ 2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9 },
            { 14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6},
           {  4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14 },
            { 11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3 }};
        static int[,] S6 = new int[4, 16] {{12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11 },
            { 10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8 },
           {  9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6},
            { 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13 } };
        static int[,] S7 = new int[4, 16] { {4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1 },
           { 13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6 },
            { 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2 },
            { 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12 } };
        static int[,] S8 = new int[4, 16] {{13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7 },
            { 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2 },
            { 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8, },
            { 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11 } };
        static int[] XOR(int[] OP1, int[] OP2, int ArrayLength) // XOR 2 binary operands
        {
            int[] Result = new int[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                if (OP1[i] != OP2[i])
                {
                    Result[i] = 1;
                }
                else
                {
                    Result[i] = 0;
                }
            }
            return Result;
        }

        static int[,] TotalKeyL = new int[16, 28]; // 2d array for all round keys left

        static int[,] TotalKeyR = new int[16, 28]; // 2d array for all rount keys right

        public static int[] convertToBinary(int optsize, int size1, int size2, int[] Inpt)
        {

            int[] Output = new int[optsize];
            int Index = 0;
            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    Output[Index] = ((Inpt[i] >> (size2 - 1) - j) & 0b01);
                    Index++;

                }
            }

            return Output;
        } //function to convert binary to individual bits in integer array

        public static int[] GenKey() //generates random key
        {
            int[] key = new int[8];
            Random rand = new Random();
            int i;
            for (i = 0; i < 8; i++)
            {
                key[i] = rand.Next(32, 126);
            }
            //key[0] = 0x5B;  // w (ASCII numbers for keyword)
            //key[1] = 0x5A; // h
            //key[2] = 0x57; // a
            //key[3] = 0x67; // t
            //key[4] = 0x6A; // e
            //key[5] = 0x56; // v 
            //key[6] = 0x67; // e
            //key[7] = 0x6E; // r
            return key;

        }

        public static int[] keyShift(int[] ip) // shifts 28 bit key halves
        {
            int[] op = new int[28];
            op[27] = ip[0];
            for (int i = 26; i >= 0; i--)
            {
                op[i] = ip[i + 1];
            }
            return op;
        }

        public static void GenAllRoundKeys(int[] L, int[] R)
        {
            int[] currentKeyL = new int[28];
            int[] currentKeyR = new int[28];
            currentKeyL = L;
            currentKeyR = R;
            for (int i = 0; i < 16; i++)
            {
                currentKeyL = keyShift(currentKeyL);
                currentKeyR = keyShift(currentKeyR);
                if (!(i == 0 || i == 1 || i == 8 || i == 15))
                {
                    currentKeyL = keyShift(currentKeyL);
                    currentKeyR = keyShift(currentKeyR); //el values eli 3ayza astore-hom fel 2d array bas ana fe loop
                }
                for (int k = 0; k < 28; k++) // el loop bt3t el total keys
                {
                    TotalKeyL[i, k] = currentKeyL[k]; // el 2d array store ???? ezay a iterate 3al row mn'3eer ma tb2a gowa el loop
                    TotalKeyR[i, k] = currentKeyR[k];
                }

            }
        } //generates all round ks

        public static int[] KeyProcess(int[] key, int roundNum, bool mode)
        {
            int[] currentKey = new int[64];
            int[] CombinedKey = new int[56];
            int[] PostPermKey = new int[56];
            int[] LKey = new int[28]; // variables for storing the split keys
            int[] RKey = new int[28];
            int[] LSKey = new int[28];
            int[] RSKey = new int[28];
            int[] PostPerm2Key = new int[48];
            ///////////////////////////Permuted Choice 1//////////////////////////
            if (roundNum == 1)
            {
                currentKey = convertToBinary(64, 8, 8, key);
                for (int i = 0; i < 56; i++)
                {
                    PostPermKey[i] = currentKey[PermChoice1[i] - 1];
                }
                for (int i = 0; i < 28; i++)
                {
                    LKey[i] = PostPermKey[i];
                    RKey[i] = PostPermKey[i + 28];
                }
                GenAllRoundKeys(LKey, RKey); //generate round keys

                if (mode == false) //if encryption
                {
                    for (int i = 0; i < 28; i++)
                    {
                        LSKey[i] = TotalKeyL[roundNum - 1, i];
                        RSKey[i] = TotalKeyR[roundNum - 1, i];
                    }
                }
                else //if decryption
                {
                    for (int i = 0; i < 28; i++)
                    {
                        LSKey[i] = TotalKeyL[16 - roundNum, i];
                        RSKey[i] = TotalKeyR[16 - roundNum, i];
                    }
                }
            }
            else //if not the first round then
            {
                if (mode == false) //if encryption
                {
                    for (int i = 0; i < 28; i++)
                    {
                        LSKey[i] = TotalKeyL[roundNum - 1, i];
                        RSKey[i] = TotalKeyR[roundNum - 1, i];

                    }
                }
                else //if decryption
                {
                    for (int i = 0; i < 28; i++)
                    {
                        LSKey[i] = TotalKeyL[16 - roundNum, i];
                        RSKey[i] = TotalKeyR[16 - roundNum, i];
                    }
                }
            }
            ///////////////Concatenate Key back//////////////////////
            LSKey.CopyTo(CombinedKey, 0);
            RSKey.CopyTo(CombinedKey, 28);
            ///////////////Permuted Choice 2/////////////////////
            for (int i = 0; i < 48; i++)
            {
                PostPerm2Key[i] = CombinedKey[PermChoice2[i] - 1];
            }
            return PostPerm2Key;
        } // Phases of Key in DES


        public static int[] MessageProcess(int[] Message, int[] Key, int roundNum)
        {
            int[] RoundMsg = new int[64];
            if (roundNum == 1) //checks mode of operation and round num
            {
                //RoundMsg = convertToBinary(64, 8, 8, Message);
                //if (flag)
                //{
                //    RoundMsg = Text_After_Round;
                //}
                Message.CopyTo(RoundMsg, 0);
            }
            else
            {
                RoundMsg = Text_After_Round;
            }

            int[] LMsg = new int[32];
            int[] Rmsg = new int[32];
            int[] ExpandedRmsg = new int[48];
            int[] PermMsg = new int[64];
            int[] IsPerm = RoundMsg;
            int[] currentMsg = new int[48];
            int[] CurrentSRow = new int[8]; // sbox row variable array
            int[] CurrentSColumn = new int[8]; // sbox column variable array
            int[] MsgPostSub = new int[8]; // array for msg after sbox
            int[] MsgPostBin = new int[32]; // array after changing the msg again to binary 
            int[] MsgPost32Perm = new int[32]; // array after 32 bit permutation p
            int[] CipherR = new int[32]; // Cipher variables
            int[] CipherL = new int[32];
            int[] Cipher = new int[64];
            int[] FinalCipher = new int[64]; // Final encrypted round message

            //PermMsg = RoundMsg;
            if (roundNum == 1) // check if it should initially permute the message as per round 1
            {
                for (int i = 0; i < 64; i++)
                {
                    PermMsg[i] = IsPerm[InitialMsgPerm[i] - 1];
                }
            }
            else
            {
                PermMsg = RoundMsg;
            }
            /////////////Splitting the message//////////////
            for (int i = 0; i < 32; i++)
            {
                LMsg[i] = PermMsg[i];
                Rmsg[i] = PermMsg[i + 32];
            }
            /////////////expansion permutation for right message half/////////
            for (int i = 0; i < 48; i++)
            {
                ExpandedRmsg[i] = Rmsg[ExpansionPerm[i] - 1];
            }
            currentMsg = XOR(ExpandedRmsg, Key, 48); /// XOR right message with the Key//////
            for (int i = 0; i < 8; i++)
            {
                CurrentSRow[i] = int.Parse(currentMsg[(i * 6)].ToString() + currentMsg[(i * 6) + 5].ToString()); //// S-box identification of rows and columns
                CurrentSRow[i] = Convert.ToInt32(CurrentSRow[i].ToString(), 2);
                for (int j = ((i * 6) + 1); j <= ((i * 6) + 4); j++)
                {
                    CurrentSColumn[i] = int.Parse(CurrentSColumn[i].ToString() + currentMsg[j].ToString());
                }
                CurrentSColumn[i] = Convert.ToInt32(CurrentSColumn[i].ToString(), 2);
            }
            MsgPostSub[0] = S1[CurrentSRow[0], CurrentSColumn[0]]; //sbox row and column choice
            MsgPostSub[1] = S2[CurrentSRow[1], CurrentSColumn[1]];
            MsgPostSub[2] = S3[CurrentSRow[2], CurrentSColumn[2]];
            MsgPostSub[3] = S4[CurrentSRow[3], CurrentSColumn[3]];
            MsgPostSub[4] = S5[CurrentSRow[4], CurrentSColumn[4]];
            MsgPostSub[5] = S6[CurrentSRow[5], CurrentSColumn[5]];
            MsgPostSub[6] = S7[CurrentSRow[6], CurrentSColumn[6]];
            MsgPostSub[7] = S8[CurrentSRow[7], CurrentSColumn[7]];
            MsgPostBin = convertToBinary(32, 8, 4, MsgPostSub);
            ////////////////// 32 bit permutation////////////////////////
            for (int i = 0; i < 32; i++)
            {
                MsgPost32Perm[i] = MsgPostBin[RightPerm[i] - 1];
            }
            ////////////////////////// XOR with left message/////////////////////////////

            CipherR = XOR(MsgPost32Perm, LMsg, 32);
            CipherL = Rmsg;
            CipherL.CopyTo(Cipher, 0);
            CipherR.CopyTo(Cipher, 32);
            if (roundNum == 16)
            {
                CipherL.CopyTo(Cipher, 32);
                CipherR.CopyTo(Cipher, 0);
                for (int r = 0; r < 64; r++)
                {
                    FinalCipher[r] = Cipher[final_message_permutation[r] - 1]; //checks if its last round to perform inverse permutation
                }
                Text_After_Round = FinalCipher;
            }
            else
            {
                FinalCipher = Cipher; //returns final cipher for round
                Text_After_Round = Cipher;
            }
            return FinalCipher;
        } // Message Phases of Encryption

        public static string BinaryIntArrayTo64String(int[] BinaryArray)
        {
            string s = "";

            for (int i = 0; i < 64; i++)
            {
                s += BinaryArray[i].ToString();
            }

            long x = Convert.ToInt64(s, 2);

            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);

            return Convert.ToBase64String(bytes);
        }

        public static string BinaryIntArrayToString(int[] BinaryArray)
        {
            string s = "";

            for (int i = 0; i < 64; i++)
            {
                s += BinaryArray[i].ToString();
            }

            long x = Convert.ToInt64(s, 2);

            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);

            string returnS = "";

            foreach (byte b in bytes)
                returnS += (char)b;

            return returnS;
        }

        public static int[] Base64StringIntoBinaryIntArray(string Base64String)
        {

            int[] BinaryIntArray = new int[64];

            byte[] bytes = Convert.FromBase64String(Base64String);

            Array.Reverse(bytes);

            BitArray Bits = new BitArray(bytes);

            for (int i = 63; i >= 0; i--)
            {
                BinaryIntArray[63 - i] = Bits[i] ? 1 : 0;
            }

            return BinaryIntArray;
        }

        static void Main(string[] args)
        {
            int roundNum;
            string pt;
            int[] FirstKey = GenKey();
            int[] RoundKey = new int[48];
            int[] RoundMessage = new int[64];
            char[] Plain_Text = new char[8];
            int[] Int_Ptext = new int[8];
            Console.WriteLine("Enter plaintext/ciphertext pls ( 8 bytes) \n");
            pt = Console.ReadLine();
            for (int i = 0; i < pt.Length; i++)
            {
                Plain_Text[i] = pt[i];
                Int_Ptext[i] = Plain_Text[i];
            }
            /////////////////////////////Encryption output/////////////////////////////////////////////////

            int[] PlainText = convertToBinary(64, 8, 8, Int_Ptext);

            for (roundNum = 1; roundNum <= 16; roundNum++)
            {
                RoundKey = KeyProcess(FirstKey, roundNum, false);
                RoundMessage = MessageProcess(PlainText, RoundKey, roundNum);
            }


            Console.WriteLine("Encryption is:\n ");

            string s = BinaryIntArrayTo64String(RoundMessage);

            Console.WriteLine(s);

            flag = false;

            int[] BackToInt = Base64StringIntoBinaryIntArray(s);

            ///////////////Decryption output/////////////////////////////////////////////
            for (roundNum = 1; roundNum <= 16; roundNum++)
            {
                RoundKey = KeyProcess(FirstKey, roundNum, true);
                RoundMessage = MessageProcess(BackToInt, RoundKey, roundNum);
            }

            Console.WriteLine("Decryption is:");

            Console.WriteLine(BinaryIntArrayToString(RoundMessage));

            Console.Read();
        }
    }
}