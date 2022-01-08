using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{

    public enum Animal
    {
        Dog = 1,
        Cat = 2,
        Bird = 3,
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class AnimalTypeAttribute : Attribute
    {
        protected Animal thePet;
        public AnimalTypeAttribute(Animal animal)
        {
            thePet = animal;
        }
        public Animal Pet
        {
            get { return thePet; }
            set { thePet = value; }
        }
    }

    class AnimalTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod() { }

        [AnimalType(Animal.Cat)]
        public void CatMethod() { }
        [AnimalType(Animal.Bird)]
        public void BirdMethod() { }
    }

    public class testAttribute1
    {
        public static void Test()
        {
            AnimalTestClass testClass = new AnimalTestClass();
            Type type = testClass.GetType();
            // Iterate through all the methods of the class.
            foreach (MethodInfo mInfo in type.GetMethods())
            {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in
                    Attribute.GetCustomAttributes(mInfo))
                {
                    // Check for the AnimalType attribute.
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                        Console.WriteLine(
                            "Method {0} has a pet {1} attribute.",
                            mInfo.Name, ((AnimalTypeAttribute)attr).Pet);
                }
            }
        }



    }
}
