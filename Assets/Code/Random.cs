[SerializableAttribute]
[ComVisibleAttribute(true)]
public class Random

///<summary>
///  This is a "random class" , by calling Random() anywhere in this file or I belive any inherited files, you should have a random number returned, another option is to call Random(int??) to get a random number with the seed of ??. i.e. if you ever call ?? again with this function you will always get the same result (Perlin Noise)
///</summary>
