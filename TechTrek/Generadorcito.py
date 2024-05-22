class Generadorcito:
    def generate_numbers_cuadrados_medios(self, semilla, iteraciones):

        seed = str(semilla)
        num_iterations = iteraciones

        # Lista que almacenará los números pseudoaleatorios generados
        ri_values = []
        # Conjunto para verificar si se generan números repetidos
        generated_numbers = set()

        # Iterar sobre el número de iteraciones especificado
        for _ in range(num_iterations):
            # Elevar al cuadrado la semilla
            seed_squared = int(seed) ** 2
            # Convertir el resultado a una cadena y ajustar su longitud
            seed_str = str(seed_squared).zfill(2 * len(seed))

            # Obtener el rango de caracteres centrales
            num_digits = len(seed)
            half_length = len(seed_str) // 2
            start_index = half_length - num_digits // 2
            end_index = half_length + num_digits // 2

            # Ajustar la longitud de la cadena si es necesario
            if end_index - start_index < num_digits:
                seed_str = seed_str.zfill(2 * num_digits)

            # Extraer la parte central de la cadena para obtener el nuevo valor
            xi_str = seed_str[start_index:end_index]
            # Convertir el valor a un número decimal en el rango [0, 1]
            xi = int(xi_str) / (10 ** num_digits)

            # Verificar si se ha generado antes el mismo número
            if xi in generated_numbers:
                # Si se detecta una secuencia repetitiva, se limpia la lista de valores generados
                ri_values = []
                return ri_values

            # Agregar el nuevo valor a la lista de valores generados
            ri_values.append(xi)
            # Agregar el nuevo valor al conjunto de números generados
            generated_numbers.add(xi)

            # Actualizar la semilla para la siguiente iteración
            seed = xi_str

        # Retornar la lista de valores generados
        return ri_values

    def generate_numbers_congruencia_lineal(self, parametroK, parametroC, parametroG, semilla2, iteraciones2):
        # Asignación de parámetros y valores iniciales
        k = parametroK
        c = parametroC
        g = parametroG
        seed = semilla2
        num_iterations = iteraciones2

        xi_values = []  # Lista para almacenar los valores de Xi
        ri_values = []  # Lista para almacenar los valores de Ri
        period_detected = False  # Bandera para indicar si se detectó un período
        period_start_index = 0  # Índice de inicio del período detectado

        # Método congruencial lineal
        for i in range(num_iterations):
            a = 1 + 2 * k  # Coeficiente multiplicativo
            m = 2 ** g  # Módulo
            seed = (a * seed + c) % m  # Fórmula para generar el próximo valor de Xi
            xi_values.append(seed)  # Agregar el nuevo valor de Xi a la lista

            # Calcular el valor de Ri
            ri_value = seed / (m - 1)
            if abs(ri_value - 1) < 1e-10 or abs(ri_value) < 1e-10:  # Manejo de casos especiales
                continue

            ri_truncated = int(ri_value * 10 ** 5) / 10 ** 5  # Truncar el valor de Ri
            ri_values.append(ri_truncated)  # Agregar el valor de Ri a la lista

            # Verificar si se ha detectado un período
            if seed in xi_values[:i]:  # Si el valor actual de Xi ya está en la lista de valores previos de Xi
                period_detected = True
                break

        if period_detected:
            period_length = len(xi_values) - period_start_index
            print("Período detectado desde la iteración", period_start_index, "hasta la iteración", len(xi_values))
            print("Longitud del período:", period_length)
            period_values = xi_values
            period_ri_values = ri_values[:len(ri_values)]

            # Guardar el período en un archivo CSV
            self.save_to_csv(period_ri_values, "numeros_ri_congruencia_periodo.csv")

        else:
            # Mostrar resultados en consola
            print("No se detectó un período en las primeras", num_iterations, "iteraciones.")

        # Guardar los resultados en un archivo CSV
        self.save_to_csv(ri_values, "numeros_ri_congruencia.csv")
