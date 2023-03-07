public interface I_GettersSetters_For_Resources{
   //G for GetAmount.
   //S for SetAmount.
   //A for AddAmount.
   //R for ReduceAmount.

   int G(string name);
   int G(int index);
   void S(string name, int amount);
   void S(int index, int amount);
   void A(string name, int amount);
   void A(int index, int amount);
   void R(string name, int amount);
   void R(int index, int amount);
}