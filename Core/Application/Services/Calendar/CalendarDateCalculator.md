# CalendarDateCalculator Documentation

## Descripción General
La clase `CalendarDateCalculator` forma parte del espacio de nombres `iPlanner.Core.Application.Services.Calendar` y proporciona métodos para realizar cálculos relacionados con fechas en un calendario. Específicamente, permite obtener el primer y último día de una semana específica en un año dado, utilizando las reglas de la cultura actual.

Esta clase está diseñada para ser utilizada en proyectos WPF que se ejecutan en .NET 8 y aprovechan las características de C# 12.0.

---

## Métodos

### `GetFirstDateOfWeek(int year, int weekOfYear)`
#### Descripción
Este método calcula la fecha correspondiente al primer día de una semana específica en un año dado. Utiliza las reglas de la cultura actual para determinar qué días pertenecen a la primera semana del año.

#### Parámetros
- `year` (int): El año para el cual se calculará la semana.
- `weekOfYear` (int): El número de la semana dentro del año (basado en la cultura actual).

#### Retorno
- `DateTime`: La fecha correspondiente al primer día de la semana especificada.

#### Detalles de Implementación
1. **Cálculo del primer jueves del año**:  
   El método comienza calculando el primer jueves del año porque, según las reglas de la mayoría de los calendarios (incluido el gregoriano), la primera semana del año es aquella que contiene al menos cuatro días. Esto asegura que la semana esté correctamente alineada con las normas internacionales (ISO 8601).

   
```csharp
   int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
   DateTime firstThursday = jan1.AddDays(daysOffset);
   
```

2. **Determinación de la primera semana**:  
   Se utiliza el método `GetWeekOfYear` del calendario de la cultura actual para determinar si la primera semana del año contiene al menos cuatro días. Si no es así, se ajusta el número de la semana.

   
```csharp
   int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
   if (firstWeek <= 1)
   {
       weekNum -= 1;
   }
   
```

3. **Cálculo del primer día de la semana**:  
   Finalmente, se calcula la fecha del primer día de la semana especificada restando tres días al jueves calculado previamente.

   
```csharp
   var result = firstThursday.AddDays(weekNum * 7);
   return result.AddDays(-3);
   
```

#### Notas
- Este método es sensible a la cultura actual del sistema, lo que significa que los resultados pueden variar dependiendo de la configuración regional.

---

### `GetLastDateOfWeek(DateTime firstDay)`
#### Descripción
Este método calcula la fecha correspondiente al último día de una semana, dado el primer día de esa semana.

#### Parámetros
- `firstDay` (DateTime): La fecha correspondiente al primer día de la semana.

#### Retorno
- `DateTime`: La fecha correspondiente al último día de la semana (6 días después del primer día).

#### Detalles de Implementación
El cálculo es directo, simplemente se suman 6 días al primer día de la semana:


```csharp
return firstDay.AddDays(6);

```

---

## Detalles Adicionales

### Importancia del Jueves en el Cálculo
El jueves se utiliza como referencia para determinar la primera semana del año porque:
1. Según la norma ISO 8601, la primera semana del año es aquella que contiene al menos cuatro días.
2. Esto asegura que la semana esté correctamente alineada con los estándares internacionales, independientemente de la cultura o región.

Por ejemplo:
- Si el 1 de enero cae en un viernes, sábado o domingo, la primera semana del año no comienza hasta el lunes siguiente.
- Si el 1 de enero cae en un lunes, martes, miércoles o jueves, esa semana se considera la primera semana del año.

El uso del jueves como punto de referencia simplifica este cálculo, ya que garantiza que siempre se evalúe una semana completa.

---

## Ejemplo de Uso


```csharp
var calculator = new CalendarDateCalculator();

// Obtener el primer día de la semana 15 del año 2025
DateTime firstDay = calculator.GetFirstDateOfWeek(2025, 15);
Console.WriteLine($"Primer día de la semana 15: {firstDay.ToShortDateString()}");

// Obtener el último día de esa semana
DateTime lastDay = calculator.GetLastDateOfWeek(firstDay);
Console.WriteLine($"Último día de la semana 15: {lastDay.ToShortDateString()}");

```

Salida esperada:


```
Primer día de la semana 15: 07/04/2025
Último día de la semana 15: 13/04/2025

```

---

## Consideraciones Técnicas
- **Compatibilidad**: Este código está diseñado para ejecutarse en .NET 8 y utiliza características de C# 12.0.
- **Cultura**: Los cálculos dependen de la configuración regional del sistema (`CultureInfo.CurrentCulture`), lo que puede afectar los resultados en diferentes entornos.

---

## Conclusión
La clase `CalendarDateCalculator` es una herramienta útil para manejar cálculos de fechas relacionados con semanas en un calendario, respetando las normas internacionales y las configuraciones culturales del sistema.