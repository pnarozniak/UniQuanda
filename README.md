# UniQuanda-back-end

## C# Naming Conventions

| Object Name                 | Notation           |
| --------------------------- | ------------------ |
| Property name               | PascalCase         |
| Public field name           | PascalCase         |
| Private field name          | \_camelCase        |
| Static field name           | PascalCase         |
| Constants variable name     | PascalCase         |
| Local variable name         | camelCase          |
| Enum Type Name              | PascalCase         |
| Enum Value Name             | PascalCase         |
| Method name                 | PascalCase         |
| Method argument name        | camelCase          |
| Interface async method name | PascalCase + Async |

<br />

| Class Name           | Notation                                          |
| -------------------- | ------------------------------------------------- |
| EfConfiguration name | PascalCase(singular class name) + EfConfiguration |
| Controller name      | PascalCase(plural class name) + Controller        |
| Repository name      | PascalCase(singular class name) + Repository      |
| Service name         | PascalCase + Service                              |
| Response DTO name    | PascalCase + ResponseDTO                          |
| Request DTO name     | PascalCase + RequestDTO                           |

## C# Coding Standards

- initialization by **= new()**
- Code documentations:
     ```html
      /// <summary>
      ///   Description
      /// </summary>
      /// <param name="param"> Description param <param>
      /// <returns> Description of each return value </returns>
    ```