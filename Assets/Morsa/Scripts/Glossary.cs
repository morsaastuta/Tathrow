using UnityEngine;

public static class Glossary
{
    public enum Property
    {
        Beginning, End,
        Confidence, Anxiety,
        Fertility, Disease,
        Courage, Rigidity,
        Power, Dependence,
        Destiny, Uncertainty,
        Esotericism, Falsehood,
        Order, Chaos,
        Gain, Loss
    }

    public static string GetProperty(Property property)
    {
        return property switch
        {
            Property.Beginning => "Comienzo",
            Property.End => "Fin",
            Property.Confidence => "Seguridad",
            Property.Anxiety => "Ansiedad",
            Property.Destiny => "Destino",
            Property.Uncertainty => "Inseguridad",
            Property.Fertility => "Fertilidad",
            Property.Disease => "Enfermedad",
            Property.Courage => "Coraje",
            Property.Rigidity => "Rigidez",
            Property.Power => "Poder",
            Property.Dependence => "Dependencia",
            Property.Esotericism => "Esotericismo",
            Property.Falsehood => "Falsedad",
            Property.Order => "Orden",
            Property.Chaos => "Caos",
            Property.Gain => "Beneficio",
            Property.Loss => "Pérdida",
            _ => ""
        };
    }

    public static Property GetRandomProperty()
    {
        return Random.Range(0,18) switch
        {
            0 => Property.Beginning,
            1 => Property.End,
            2 => Property.Confidence,
            3 => Property.Anxiety,
            4 => Property.Destiny,
            5 => Property.Uncertainty,
            6 => Property.Fertility,
            7 => Property.Disease,
            8 => Property.Courage,
            9 => Property.Rigidity,
            10 => Property.Power,
            11 => Property.Dependence,
            12 => Property.Esotericism,
            13 => Property.Falsehood,
            14 => Property.Order,
            15 => Property.Chaos,
            16 => Property.Gain,
            17 => Property.Loss
        };
    }
}
