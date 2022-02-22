
#!/usr/bin/env python
# coding: utf-8

# # MGR

# ## 2. Scale and split data

import numpy as np
import matplotlib.pyplot as plt
from utils import Helper

# Check correlation:

# In[22]:

Helper.plot_corr(tracks)


# Shuffle data:

# In[23]:


from sklearn.utils import shuffle
tracks = shuffle(tracks)


# Scale and fit data:

# In[24]:


def clean_dataset(df):
    assert isinstance(df, pd.DataFrame), "df needs to be a pd.DataFrame"
    df.dropna(inplace=True)
    indices_to_keep = ~df.isin([np.nan, np.inf, -np.inf]).any(1)
    return df[indices_to_keep].astype(np.float64)


# In[25]:


from sklearn.preprocessing import StandardScaler

data = tracks[['genre','tempo', 'power',
               'mfcc_1','mfcc_2','mfcc_3','mfcc_4','mfcc_5','mfcc_6','mfcc_7','mfcc_8','mfcc_9','mfcc_10','mfcc_11','mfcc_12',
               'band_1','band_2','band_3','band_4','band_5','band_6','band_7','band_8','band_9','band_10','band_11']]

data = clean_dataset(data)

scaler = StandardScaler()
scaler.fit_transform(data)


# ## 3. Create and train model
# ### A. Using band powers as inputs

# Select input and result sets from dataframe:

# In[26]:


y = data[['band_1','band_2','band_3','band_4','band_5','band_6','band_7','band_8','band_9','band_10','band_11']]
X = data[['genre','tempo',
         'band_1','band_2','band_3','band_4','band_5','band_6','band_7','band_8','band_9','band_10','band_11']]


# Split datasets to train (80%) and test (20%):

# In[27]:


from sklearn.model_selection import train_test_split

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)


# Check shapes of train and test datasets:

# In[28]:


print(X_train.shape)
print(y_train.shape)
print(X_test.shape)
print(y_test.shape)


# Create model:

# In[29]:


from keras.models import Sequential
from keras.layers import Dense

model = Sequential()

model.add(Dense(units=22, input_dim=13, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=44, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=11, kernel_initializer='normal'))

model.compile(loss='mean_squared_error', optimizer='adam', metrics=['accuracy'])

model.summary()


# Train model:

# In[30]:


history = model.fit(X_train, y_train, batch_size=10, epochs = 100, verbose=1)


# Check MSE values:

# In[31]:


scores = model.evaluate(X_train, y_train, verbose=0)
for i in range(len(scores)):
    print("%s: %.2f%%" % (model.metrics_names[i], scores[i]*100))


# Plot loss during training:

# In[32]:


plt.title('Loss / Mean Squared Error')
plt.plot(history.history['loss'], label='train')
plt.legend()
plt.show()


# In[33]:


plt.title('Accuracy / Mean Squared Error')
plt.plot(history.history['accuracy'], label='train')
plt.legend()
plt.show()


# In[34]:


y_predict = model.predict(X_test)


# In[35]:


print(y_predict[0])


# In[36]:


print(y_test.head(1))


# Export CNN to H5 file:

# In[37]:


model_json = model.to_json()
with open(r"D:\MGR\fma\model_A.json", "w") as json_file:
    json_file.write(model_json)

# serialize weights to HDF5
model.save_weights(r"D:\MGR\fma\model_A.h5")


# ### B. Using overall track power as input

# Select input and result sets from dataframe:

# In[38]:


y = data[['band_1','band_2','band_3','band_4','band_5','band_6','band_7','band_8','band_9','band_10','band_11']]
X = data[['genre','tempo','power']]


# Split datasets to train (80%) and test (20%):

# In[39]:


from sklearn.model_selection import train_test_split

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)


# Check shapes of train and test datasets:

# In[40]:


print(X_train.shape)
print(y_train.shape)
print(X_test.shape)
print(y_test.shape)


# Create model:

# In[41]:


model = Sequential()

model.add(Dense(units=22, input_dim=3, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=44, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=11, kernel_initializer='normal'))

model.compile(loss='mean_squared_error', optimizer='adam', metrics=['accuracy'])

model.summary()


# Train model:

# In[42]:


history = model.fit(X_train, y_train, batch_size=10, epochs = 100, verbose=1)


# Check MSE values:

# In[43]:


scores = model.evaluate(X_train, y_train, verbose=0)
for i in range(len(scores)):
    print("%s: %.2f%%" % (model.metrics_names[i], scores[i]*100))


# Plot loss during training:

# In[44]:


plt.title('Loss / Mean Squared Error')
plt.plot(history.history['loss'], label='train')
plt.legend()
plt.show()


# In[45]:


plt.title('Accuracy / Mean Squared Error')
plt.plot(history.history['accuracy'], label='train')
plt.legend()
plt.show()


# In[46]:


y_predict = model.predict(X_test)


# In[47]:


print(y_predict[0])


# In[48]:


print(y_test.head(1))


# Export CNN to H5 file:

# In[49]:


model_json = model.to_json()
with open(r"D:\MGR\fma\model_B.json", "w") as json_file:
    json_file.write(model_json)

# serialize weights to HDF5
model.save_weights(r"D:\MGR\fma\model_B.h5")


# ### C. Using MFCCs as inputs

# Select input and result sets from dataframe:

# In[50]:


y = data[['band_1','band_2','band_3','band_4','band_5','band_6','band_7','band_8','band_9','band_10','band_11']]
X = data[['genre','tempo',
         'mfcc_1','mfcc_2','mfcc_3','mfcc_4','mfcc_5','mfcc_6','mfcc_7','mfcc_8','mfcc_9','mfcc_10','mfcc_11','mfcc_12']]


# Split datasets to train (80%) and test (20%):

# In[51]:


from sklearn.model_selection import train_test_split

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)


# Check shapes of train and test datasets:

# In[52]:


print(X_train.shape)
print(y_train.shape)
print(X_test.shape)
print(y_test.shape)


# Create model:

# In[53]:


model = Sequential()

model.add(Dense(units=22, input_dim=14, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=44, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=11, kernel_initializer='normal'))

model.compile(loss='mean_squared_error', optimizer='adam', metrics=['accuracy'])

model.summary()


# Train model:

# In[54]:


history = model.fit(X_train, y_train, batch_size=10, epochs = 100, verbose=1)


# Check MSE values:

# In[55]:


scores = model.evaluate(X_train, y_train, verbose=0)
for i in range(len(scores)):
    print("%s: %.2f%%" % (model.metrics_names[i], scores[i]*100))


# Plot loss during training:

# In[56]:


plt.title('Loss / Mean Squared Error')
plt.plot(history.history['loss'], label='train')
plt.legend()
plt.show()


# In[57]:


plt.title('Accuracy / Mean Squared Error')
plt.plot(history.history['accuracy'], label='train')
plt.legend()
plt.show()


# In[58]:


y_predict = model.predict(X_test)


# In[59]:


print(y_predict[0])


# In[60]:


print(y_test.head(1))


# Export CNN to H5 file:

# In[61]:


model_json = model.to_json()
with open(r"D:\MGR\fma\model_C.json", "w") as json_file:
    json_file.write(model_json)

# serialize weights to HDF5
model.save_weights(r"D:\MGR\fma\model_C.h5")

