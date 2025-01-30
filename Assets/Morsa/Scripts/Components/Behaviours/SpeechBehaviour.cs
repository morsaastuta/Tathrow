using DG.Tweening;
using TMPro;
using UnityEngine;
using static Glossary;

public class SpeechBehaviour : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] TextMeshPro speech;
    [SerializeField] int timeline;

    public void Show(Property p)
    {
        bubble.transform.DOScale(new Vector3(1, 1, 1), 0.2f);

        string sp = "";

        switch(timeline)
        {
            case -1:
                switch (p)
                {
                    case Property.Anxiety: sp = "He pasado por episodios muy agotadores mentalmente..."; break;
                    case Property.Beginning: sp = "Hace un tiempo cambié mi estilo de vida."; break;
                    case Property.Chaos: sp = "He tenido un crecimiento algo problemático."; break;
                    case Property.Confidence: sp = "Siempre me he sentido apoyado."; break;
                    case Property.Courage: sp = "Nunca me ha faltado valor para hacer frente a mis miedos."; break;
                    case Property.Dependence: sp = "Mis padres han sido siempre muy estrictos."; break;
                    case Property.Destiny: sp = "Siempre he sabido a lo que debía dedicarme."; break;
                    case Property.Disease: sp = "De chico siempre fui muy enfermizo."; break;
                    case Property.End: sp = "He desconectado con parte de mi pasado."; break;
                    case Property.Esotericism: sp = "He tenido revelaciones misteriosas en el pasado."; break;
                    case Property.Falsehood: sp = "Más de una vez, amigos íntimos me han traicionado."; break;
                    case Property.Fertility: sp = "Creciendo, nunca me faltó de nada."; break;
                    case Property.Gain: sp = "Siempre he conseguido lo que me he propuesto."; break;
                    case Property.Loss: sp = "Pasé por muchos funerales creciendo."; break;
                    case Property.Order: sp = "Creciendo, he sido una persona muy organizada."; break;
                    case Property.Power: sp = "Siempre he hecho lo que he querido, nadie ha sido capaz de impedírmelo."; break;
                    case Property.Rigidity: sp = "Nunca he cambiado mi forma de ser, así estoy a gusto."; break;
                    case Property.Uncertainty: sp = "He dejado muchos cabos sueltos atrás."; break;
                }
                break;

            case 0:
                switch (p)
                {
                    case Property.Anxiety: sp = "Algo malo está a punto de ocurrirme, estoy seguro."; break;
                    case Property.Beginning: sp = "Acabo de llegar a esta ciudad, ¿qué me depara el futuro?"; break;
                    case Property.Chaos: sp = "Mi vida está dando demasiadas vueltas."; break;
                    case Property.Confidence: sp = "Todo me está saliendo bien, siento que puedo con todo."; break;
                    case Property.Courage: sp = "No paro de superar obstáculos, y creo que podría seguir así siempre."; break;
                    case Property.Dependence: sp = "No soy capaz de dejar la casa de mis padres."; break;
                    case Property.Destiny: sp = "No sé qué hacer con mi vida, quizá aquí encuentre respuestas."; break;
                    case Property.Disease: sp = "Justo ayer pillé una gripe, vaya invierno más feo."; break;
                    case Property.End: sp = "Estoy preparándome para emigrar."; break;
                    case Property.Esotericism: sp = "Creo que algo o alguien está observándome desde más allá..."; break;
                    case Property.Falsehood: sp = "La gente de mi alrededor actúa de forma confusa, no confío en ellos."; break;
                    case Property.Fertility: sp = "Mi pareja y yo estamos esperando un bebé."; break;
                    case Property.Gain: sp = "He conseguido un trabajo recientemente."; break;
                    case Property.Loss: sp = "Mi madre murió en un accidente hace poco, no sé cómo salir adelante."; break;
                    case Property.Order: sp = "Todo sigue como debería estar."; break;
                    case Property.Power: sp = "Muchos amigos me deben favores, pero me gusta acumularlos."; break;
                    case Property.Rigidity: sp = "Intento que todo se mantenga de la forma que siempre ha sido."; break;
                    case Property.Uncertainty: sp = "No sé si estoy haciendo las cosas de la mejor manera..."; break;
                }
                break;

            case 1:
                switch (p)
                {
                    case Property.Anxiety: sp = "Cada vez tengo más cosas que hacer, no doy a basto."; break;
                    case Property.Beginning: sp = "Espero que mudarme no sea dejar todo lo que aquí tengo."; break;
                    case Property.Chaos: sp = "Veo muy negro el futuro de este mundo nuestro."; break;
                    case Property.Confidence: sp = "Tengo la sensación de que todo irá bien."; break;
                    case Property.Courage: sp = "Querría superar mis miedos de una vez por todas..."; break;
                    case Property.Dependence: sp = "Creo que este trabajo me va a quitar más tiempo de lo que el salario compensa."; break;
                    case Property.Destiny: sp = "Espero cumplir mi propósito."; break;
                    case Property.Disease: sp = "Cada vez puedo comprar menos comida..."; break;
                    case Property.End: sp = "No parece que vaya a durar mucho más en mi puesto..."; break;
                    case Property.Esotericism: sp = "Siento que algo mágico está a punto de ocurrir."; break;
                    case Property.Falsehood: sp = "El viaje que gané en la lotería no me huele muy legal."; break;
                    case Property.Fertility: sp = "Parece que el próximo invierno habrá buena cosecha."; break;
                    case Property.Gain: sp = "Querría cumplir mis objetivos."; break;
                    case Property.Loss: sp = "Mi relación con mis padres va cada vez a peor, no creo que merezca la pena seguir así."; break;
                    case Property.Order: sp = "Ojalá todo esté bajo control..."; break;
                    case Property.Power: sp = "Me gustaría ser capaz de hacer frente a todo lo que se me viene encima."; break;
                    case Property.Rigidity: sp = "No creo que las cosas cambien en el futuro."; break;
                    case Property.Uncertainty: sp = "Pienso mucho en qué podrá pasar el día de mañana."; break;
                }
                break;
        }

        speech.SetText(sp);
    }

    public void Hide()
    {
        bubble.transform.DOScale(new Vector3(0, 0, 1), 0.2f);
    }
}
