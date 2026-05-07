# Estructura actual y posibles usos

## Visión general

La solución está organizada en cuatro proyectos principales con una separación por capas pensada para crecer hacia un servicio de resumen de archivos con LLM.

## Estructura actual

- Api.slnx
  - Contiene la solución y referencia a los cuatro proyectos.
- Api/
  - Proyecto ASP.NET Core.
  - Actualmente incluye un endpoint de ejemplo para clima.
  - Es la puerta de entrada HTTP para clientes externos.
- Application/
  - Biblioteca para casos de uso y orquestación.
  - En el estado actual está preparada para recibir lógica de aplicación.
- Domain/
  - Biblioteca destinada a reglas de negocio y modelos del dominio.
  - En el estado actual está como base mínima.
- Infrastructure/
  - Biblioteca pensada para integraciones técnicas.
  - En el estado actual está como base mínima.

## Posibles usos por proyecto

### Api

Posibles usos:

- Exponer endpoint para carga de archivo y solicitud de resumen.
- Publicar documentación OpenAPI para pruebas desde Swagger.
- Centralizar validación de entrada a nivel HTTP.

Ejemplos de endpoints futuros:

- POST /summaries: recibe un archivo y devuelve un resumen.
- GET /health: estado del servicio.

### Application

Posibles usos:

- Definir casos de uso como resumir archivo, resumir texto o resumir lote de documentos.
- Coordinar validaciones funcionales y transformación de datos.
- Declarar contratos para proveedores LLM sin depender de implementación concreta.

Ejemplos de componentes futuros:

- Caso de uso para resumen corto, medio o detallado.
- Servicio de orquestación de prompt según tipo de archivo.

### Domain

Posibles usos:

- Modelar conceptos de negocio como Documento, Resumen y Política de resumen.
- Aplicar reglas de negocio independientes de frameworks.
- Definir invariantes como longitud máxima o formato de salida esperado.

Ejemplos de valor:

- Mantener consistencia de resultados.
- Facilitar pruebas puras de negocio.

### Infrastructure

Posibles usos:

- Implementar cliente HTTP para Ollama como proveedor principal.
- Implementar proveedores alternos en caso de fallback.
- Manejar detalles técnicos: timeout, retry, logging técnico y serialización.

Ejemplos de integraciones futuras:

- Ollama local para ejecución en entorno de desarrollo.
- Proveedor remoto opcional para continuidad operativa.

## Posibles usos del sistema completo

1. Resumen automático de documentos internos.
2. Preprocesamiento de archivos para flujos de soporte o análisis.
3. Generación de extractos para paneles administrativos.
4. Integración con otros sistemas mediante API REST.

## Estado actual y próximo paso recomendado

Estado actual:

- La arquitectura base existe, pero la funcionalidad de resumen aún no está implementada.

Próximo paso recomendado:

- Crear el endpoint de carga en Api y un caso de uso en Application que invoque un contrato de resumen, cuya primera implementación viva en Infrastructure usando Ollama.
