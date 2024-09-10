"""
Usage:
    python3 test_smtp.py
"""
import smtplib
from email.mime.text import MIMEText

def send_test_email(sender_email, recipient_email, subject, body, app_password):
  """
  Sends a test email using Gmail SMTP with app password.

  Args:
    sender_email: The sender's email address.
    recipient_email: The recipient's email address.
    subject: The subject of the email.
    body: The body of the email.
    app_password: The Gmail app password for your application.
  """

  try:
    # Connect to Gmail SMTP server using STARTTLS for security
    server = smtplib.SMTP('smtp.gmail.com', 587)
    server.starttls()

    # Login using app password
    server.login(sender_email, app_password)

    # Create a message object
    message = MIMEText(body)
    message['From'] = sender_email
    message['To'] = recipient_email
    message['Subject'] = subject

    # Send the email
    server.sendmail(sender_email, recipient_email, message.as_string())

    print('Email sent successfully!')
  except Exception as e:   

    print('Error sending email:', e)

# Replace with your Gmail credentials and app password
sender_email = 'your_gmail_address@gmail.com'
recipient_email = 'recipient_email@example.com'
subject = 'Test Email'
body = 'This is a test email sent using Gmail SMTP.'
app_password = 'your_gmail_app_password'  # Replace with your generated app password

send_test_email(sender_email, recipient_email, subject, body, app_password)