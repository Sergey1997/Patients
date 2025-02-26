# Patients

graph TD;
    User[👤 Пользователь] -->|Создает агента| AI_Agent[🤖 AI Агент]
    AI_Agent -->|Сохраняется в Supabase| Supabase_DB[(🗄️ Supabase DB)]
    AI_Agent -->|Отправляет твиты| Twitter_API((🐦 Twitter API))
    AI_Agent -->|Лайки и реплаи| Twitter_API
    
    User -->|Запрос на создание агента| Frontend[💻 React UI]
    Frontend -->|Запрос к API| Supabase_Functions[⚡ Supabase Edge Functions]
    Supabase_Functions -->|Генерация контента| OpenAI_API((🧠 OpenAI API))
    
    User -->|Подключает кошелек| Web3_Auth[🔑 Web3 Auth]
    Web3_Auth -->|Отправляет транзакции| Base_Blockchain[(⛓️ Base Blockchain)]
    
    Supabase_DB -->|Синхронизация данных| Frontend
    Supabase_Functions -->|Минтинг токенов| Base_SmartContracts[📜 Smart Contracts (Base)]
    Base_SmartContracts -->|Выпуск токена агента| Token[🏷️ NFT/ERC-20]
    Token -->|Покупка/Продажа| Marketplace[💰 Маркетплейс]
    
    Marketplace -->|Смена владельца| Base_SmartContracts
    Base_SmartContracts -->|Обновление владельца агента| Supabase_DB
    
    Twitter_API -->|Публикация успешна| Supabase_DB
    Supabase_DB -->|Обновление UI| Frontend
