# Documentación del Proyecto

## Resumen

Este repositorio contiene una solución .NET organizada en cuatro proyectos:

- `Api`: capa de entrada HTTP.
- `Application`: capa de casos de uso y orquestación.
- `Domain`: núcleo del negocio.
- `Infrastructure`: integraciones técnicas y servicios externos.

La idea funcional del sistema es construir una API que reciba un archivo, procese su contenido y devuelva un resumen generado por un LLM. El proveedor principal previsto es Ollama, por lo que el flujo debe intentar primero ese backend y, si no está disponible, permitir una estrategia alternativa o un manejo controlado del error.

## Estado actual del repositorio

Hoy la solución está en una etapa inicial. La API contiene únicamente el template por defecto de ASP.NET Core con el endpoint de ejemplo `/weatherforecast`. Los otros proyectos existen como esqueletos vacíos, sin clases de dominio, contratos, servicios ni referencias entre capas.

Eso significa que la arquitectura está definida a nivel de estructura de solución, pero la implementación funcional todavía no está construida.

## Arquitectura propuesta

La solución sigue una separación por capas que puede evolucionar hacia una arquitectura limpia o una variante modular simple:

### 1. API

Es la capa de exposición. Debe encargarse de:

- recibir la petición HTTP con el archivo,
- validar el formato y tamaño del archivo,
- invocar un caso de uso de Application,
- devolver la respuesta resumida en JSON.

No debería contener lógica de negocio ni llamadas directas al proveedor LLM.

### 2. Application

Es la capa de orquestación. Debe contener:

- casos de uso para resumir archivos,
- contratos o interfaces que la infraestructura implementa,
- DTOs o modelos de entrada y salida,
- reglas de flujo, validaciones funcionales y manejo de errores de caso de uso.

Aquí se decide cómo se procesa el archivo y cómo se prepara el prompt para el modelo.

### 3. Domain

Es el núcleo del negocio. Debe contener:

- entidades o value objects si el problema lo requiere,
- reglas de negocio puras,
- abstracciones conceptuales del dominio.

Si el caso de uso se mantiene simple, esta capa puede ser mínima, pero debe conservar la intención de negocio separada de detalles técnicos.

### 4. Infrastructure

Es la capa técnica. Debe implementar:

- el cliente para Ollama,
- acceso a archivos o almacenamiento temporal si se necesita,
- adaptadores para integraciones externas,
- configuración técnica, timeouts, reintentos y serialización.

Esta capa no debe definir reglas del negocio, solo ejecutar detalles de infraestructura.

## Flujo funcional esperado

1. El cliente envía un archivo a la API.
2. La API valida la solicitud y la transforma en un comando o request de Application.
3. Application extrae o prepara el contenido del archivo.
4. Application llama a un servicio abstracto de resumen.
5. Infrastructure usa Ollama como primera opción para generar el resumen.
6. La API retorna el resultado final al consumidor.

## Estructura actual de la solución

- `Api.slnx`: solución principal.
- `Api/`: proyecto web con el punto de entrada.
- `Application/`: proyecto preparado para casos de uso.
- `Domain/`: proyecto para el núcleo del negocio.
- `Infrastructure/`: proyecto para integraciones.

## Observaciones técnicas

- La solución usa `net10.0` en todos los proyectos.
- La API tiene habilitado OpenAPI en desarrollo.
- No hay todavía dependencias entre proyectos.
- No existen endpoints para carga de archivos ni integración con Ollama.

## Qué debería documentarse cuando avance la implementación

- contrato del endpoint de subida de archivo,
- formato de respuesta del resumen,
- configuración de Ollama,
- comportamiento cuando Ollama no esté disponible,
- límites de tamaño y tipos de archivo soportados,
- estrategia de logging y trazabilidad.
