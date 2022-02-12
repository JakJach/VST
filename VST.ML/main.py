import pandas as pd
from sklearn.utils import shuffle

# Import data from csv file
data = pd.read_csv(r'D:\MGR\fma\tracks.csv')

# Set index on track_id column
data = data.set_index('track_id')

# Shuffle files randomly
data = shuffle(data)

# Split data into test (20%) and train (80%) dataframes
percentage = round(len(data)*0.8)
train_data = data.head(percentage)
test_data = data.iloc[percentage:len(data),:]

print(test_data.head(10))