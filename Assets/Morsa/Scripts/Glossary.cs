
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
}
