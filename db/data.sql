USE pharmacy;

INSERT INTO role (role_name) VALUES
('Client'), 
('Admin');  

INSERT INTO users (login, email, password_hash, phone_number)
VALUES (
  'testuser1',
  'testuser1@gmail.com',
  '$2y$10$e0MYzXyjpJS7Pd0RVvHwHeOX5jFJwTh5Df.Yb4uLG2zZG9f6Jf2Hm',
  '+380501234567'
);

INSERT INTO email_verification_tokens (user_id, token, expires_at)
VALUES (
    1,   
    'd03c6e2b-9f1b-4b5f-93c5-5e27a1e3f2da', 
    DATE_ADD(NOW(), INTERVAL 2 MINUTE)
);

INSERT INTO catalog (catalog_name) VALUES
('Знеболюючі'),
('Антибіотики'),
('Противірусні препарати'),
('Протизапальні засоби'),
('Вітаміни та мінерали'),
('Засоби від застуди та грипу'),
('Серцево-судинні засоби'),
('Препарати для очей, вух, носа');

INSERT INTO company (company_name, email, country) VALUES
('Biomedica Ukraine', 'info@biomedica.ua', 'Ukraine'),
('Global Pharm Group', 'contact@globalpharm.com', 'USA'),
('InterMed Labs', 'office@intermedlabs.de', 'Germany'),
('PharmaCorn International', 'sales@pharmacorn.in', 'India'),
('NovaMed Technologies', 'info@novamed.pl', 'Poland'),
('VitalPharm Bio', 'support@vitalbio.fr', 'France'),
('Medikos Alliance', 'admin@medikos-alliance.ca', 'Canada'),
('Greenpharma Plus', 'hello@greenpharma.it', 'Italy'),
('Influmed Ltd', 'info@influmed.co.uk', 'United Kingdom'),
('Cardiopharm Systems', 'cardio@pharmsystems.es', 'Spain');

INSERT INTO product (name, description, price, purchase_price, purchase_date, quantity, image_url, company_id, catalog_id)
VALUES
('Ібупрофен 400 мг', 'Знеболюючий та протизапальний засіб', 68.00, 35.00, '2025-04-20', 120, 'images/Ібупрофен.jpg', 1, 1),
('Амоксицилін 500 мг', 'Антибіотик широкого спектру', 120.00, 70.00, '2025-03-18', 80, 'images/Амоксицилін.png', 6, 2),
('Ацикловір 200 мг', 'Противірусний препарат для лікування герпесу', 95.00, 55.00, '2025-04-01', 60, 'images/Ацикловір.jpg', 4, 3),
('Диклофенак 100 мг', 'Протизапальний засіб для суглобів і м’язів', 75.00, 40.00, '2025-04-12', 100, 'images/Диклофенак.jpg', 9, 4),
('Вітамін C 500 мг', 'Підтримка імунної системи', 50.00, 22.00, '2025-03-25', 200, 'images/Вітамін C.png', 8, 5),
('Терафлю порошок', 'Комплексний засіб від застуди та грипу', 88.00, 50.00, '2025-04-02', 90, 'images/Терафлю порошок.png', 3, 6),
('Аспірин Кардіо', 'Засіб для підтримки серцево-судинної системи', 110.00, 65.00, '2025-04-15', 130, 'images/Аспірин Кардіо.jpg', 7, 7),
('Візін Класік', 'Краплі для зняття почервоніння очей', 70.00, 30.00, '2025-03-30', 50, 'images/Візін Класік.jpg', 2, 8),
('Назол Бебі', 'Судинозвужувальні краплі для носа', 60.00, 28.00, '2025-04-18', 100, 'images/Назол Бебі.jpg', 5, 8),
('Панадол Екстра', 'Знеболюючий препарат з кофеїном', 95.00, 55.00, '2025-04-10', 150, 'images/Панадол Екстра.jpg', 10, 1);