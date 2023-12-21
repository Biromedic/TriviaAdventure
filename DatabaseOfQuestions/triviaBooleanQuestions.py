import sqlite3
import requests


api_url = "https://opentdb.com/api.php?amount=50&difficulty=easy&type=boolean"  

conn = sqlite3.connect('questions.db')
cursor = conn.cursor()

cursor.execute('''
    CREATE TABLE IF NOT EXISTS true_false_questions (
        id INTEGER PRIMARY KEY,
        question TEXT NOT NULL,
        is_true BOOLEAN NOT NULL
    )
''')

response = requests.get(api_url)
api_data = response.json()

api_questions = api_data.get('results', [])

for questions in api_questions:
    question_text = questions.get('question', '')
    is_true = questions.get('correct_answer', '').lower() == 'true'  

    cursor.execute('''
        INSERT INTO true_false_questions (question, is_true)
        VALUES (?, ?)
    ''', (question_text, is_true))

conn.commit()
conn.close()