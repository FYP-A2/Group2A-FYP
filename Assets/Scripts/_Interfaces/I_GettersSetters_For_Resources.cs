public interface I_GettersSetters_For_Resources{
   //G for GetAmount.
   //S for SetAmount.
   //A for AddAmount.
   //R for ReduceAmount.

   int GetAmount(string name);
   int GetAmount(int index);
   void SetAmount(string name, int amount);
   void SetAmount(int index, int amount);
   void Add(string name, int amount);
   void Add(int index, int amount);
   void Reduce(string name, int amount);
   void Reduce(int index, int amount);
}