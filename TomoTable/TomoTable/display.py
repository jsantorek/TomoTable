# -*- coding: utf-8 -*-
"""
Created on Mon Jun 12 09:55:59 2017

@author: Jakub Santorek
"""


import matplotlib.pyplot as plt
import numpy as np

fpath_data = 'training/input/'

zero = np.loadtxt(fname=fpath_data+'0 bb.txt', skiprows=1)
sero = zero + np.transpose(zero)
print(sero)

fig = plt.figure()
ax = fig.add_subplot(1,1,1)
ax.set_aspect('equal')
plt.imshow(sero, interpolation='nearest', cmap=plt.cm.ocean)
plt.colorbar()
plt.show()

"""
x = np.arange(0, 10, 0.2)
y = np.sin(x)
fig = plt.figure()
ax = fig.add_subplot(111)
ax.plot(x, y)
plt.show()

function mat_=vect(a, n, m)
%a - size 690x1
%n - 30
%m - 23
for i=0:m-1 %--> 0...29
   for j=1:n
      mat_(i+1,j)=a(j+i*n);
   end
end
"""