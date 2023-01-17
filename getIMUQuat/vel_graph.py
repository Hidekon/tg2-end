from sklearn.linear_model import LinearRegression
import numpy as np
import matplotlib.pyplot as plt

# Set the x and y values for your data set
x = np.array([0.29, 0.38, 0.51,  0.60,  0.67,   0.78,  0.85]).reshape(-1,1)
y = np.array([   1,  1.5,    2,   2.5,     3,    3.5,     4])

# Create a LinearRegression object
reg = LinearRegression()

# Fit the model to the data
reg.fit(x, y)

# Print the coefficients
print("Coefficients:", reg.coef_)
print("Intercept:", reg.intercept_)

# Predict the y values for the x values
y_pred = reg.predict(x)

# Plot the data with the regression line
plt.scatter(x, y, color = "m", marker = "o", s = 30)
plt.plot(x, y_pred, color = "g")
plt.ylabel('Velocidade Real (km/h)')
plt.xlabel('Frequência (ciclo/seg)')
plt.show()