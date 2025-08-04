# 📊 Test Data Guide - Stock Management System

## 🎯 Overview

This guide explains the comprehensive test data that has been added to the Stock Management System database. The system now includes 10 test records for each major table, providing a realistic dataset for testing and demonstration purposes.

---

## 📋 Database Tables and Test Data

### 1. **Categories** (10 Records)
| ID | Name | Description |
|----|------|-------------|
| 1 | Electronics | Electronic devices and components |
| 2 | Office Supplies | Office stationery and supplies |
| 3 | Furniture | Office furniture and fixtures |
| 4 | IT Equipment | Computers, servers, and networking equipment |
| 5 | Tools | Hand tools and power tools |
| 6 | Safety Equipment | Personal protective equipment |
| 7 | Cleaning Supplies | Cleaning materials and chemicals |
| 8 | Packaging | Packaging materials and containers |
| 9 | Automotive | Automotive parts and accessories |
| 10 | Medical Supplies | Medical equipment and supplies |

### 2. **Products** (10 Records)
| ID | Name | SKU | Category | UOM | Stock Qty | Reorder Level | Unit Price | Description |
|----|------|-----|----------|-----|-----------|---------------|------------|-------------|
| 1 | Laptop Dell XPS 13 | LAP-DELL-XPS13 | Electronics | PCS | 25 | 5 | $1,299.99 | 13-inch premium laptop |
| 2 | Wireless Mouse | ACC-MOUSE-WL01 | Electronics | PCS | 150 | 20 | $29.99 | Bluetooth wireless mouse |
| 3 | Office Chair | FUR-CHAIR-EX01 | Furniture | PCS | 12 | 3 | $299.99 | Ergonomic office chair |
| 4 | Network Switch | IT-SWITCH-24P | IT Equipment | PCS | 8 | 2 | $199.99 | 24-port network switch |
| 5 | Screwdriver Set | TOOL-SCREW-10P | Tools | SET | 30 | 5 | $49.99 | 10-piece screwdriver set |
| 6 | Safety Helmet | SAFE-HELM-YEL | Safety Equipment | PCS | 45 | 10 | $39.99 | Yellow safety helmet |
| 7 | All-Purpose Cleaner | CLEAN-AP-1L | Cleaning Supplies | BOTTLE | 60 | 15 | $8.99 | 1L all-purpose cleaner |
| 8 | Cardboard Boxes | PACK-BOX-30CM | Packaging | PCS | 200 | 50 | $2.99 | 30cm cardboard boxes |
| 9 | Car Battery | AUTO-BAT-12V | Automotive | PCS | 15 | 3 | $89.99 | 12V car battery |
| 10 | First Aid Kit | MED-FAK-STD | Medical Supplies | KIT | 20 | 5 | $45.99 | Standard first aid kit |

### 3. **GRNs (Goods Received Notes)** (10 Records)
| ID | Supplier | Date | Status | Total Amount |
|----|----------|------|--------|--------------|
| 1 | TechCorp Inc. | -30 days | Completed | $3,249.75 |
| 2 | OfficeMax Solutions | -25 days | Completed | $899.70 |
| 3 | Furniture World | -20 days | Completed | $1,799.94 |
| 4 | Network Pro | -15 days | Completed | $1,599.92 |
| 5 | ToolMaster Ltd. | -10 days | Completed | $1,499.70 |
| 6 | Safety First Co. | -5 days | Completed | $1,799.55 |
| 7 | CleanPro Supplies | -3 days | Completed | $539.40 |
| 8 | Packaging Plus | -2 days | Completed | $598.00 |
| 9 | AutoParts Express | -1 day | Completed | $1,349.85 |
| 10 | Medical Supplies Co. | Today | Pending | $919.80 |

### 4. **GRN Items** (12 Records)
| ID | GRN ID | Product | Quantity | Unit Price |
|----|--------|---------|----------|------------|
| 1 | 1 | Laptop Dell XPS 13 | 2 | $1,299.99 |
| 2 | 1 | Wireless Mouse | 5 | $29.99 |
| 3 | 2 | Wireless Mouse | 20 | $29.99 |
| 4 | 2 | All-Purpose Cleaner | 10 | $8.99 |
| 5 | 3 | Office Chair | 6 | $299.99 |
| 6 | 4 | Network Switch | 8 | $199.99 |
| 7 | 5 | Screwdriver Set | 30 | $49.99 |
| 8 | 6 | Safety Helmet | 45 | $39.99 |
| 9 | 7 | All-Purpose Cleaner | 60 | $8.99 |
| 10 | 8 | Cardboard Boxes | 200 | $2.99 |
| 11 | 9 | Car Battery | 15 | $89.99 |
| 12 | 10 | First Aid Kit | 20 | $45.99 |

### 5. **GINs (Goods Issued Notes)** (10 Records)
| ID | Recipient | Date | Reason |
|----|-----------|------|--------|
| 1 | IT Department | -28 days | New employee setup |
| 2 | Marketing Team | -23 days | Campaign materials |
| 3 | HR Department | -18 days | Office renovation |
| 4 | Operations Team | -13 days | Network upgrade |
| 5 | Maintenance Crew | -8 days | Equipment repair |
| 6 | Construction Team | -3 days | Safety equipment |
| 7 | Janitorial Staff | -1 day | Cleaning supplies |
| 8 | Shipping Department | Today | Packaging materials |
| 9 | Fleet Management | Today | Vehicle maintenance |
| 10 | Medical Team | Today | Emergency supplies |

### 6. **GIN Items** (12 Records)
| ID | GIN ID | Product | Quantity |
|----|--------|---------|----------|
| 1 | 1 | Laptop Dell XPS 13 | 1 |
| 2 | 1 | Wireless Mouse | 2 |
| 3 | 2 | Wireless Mouse | 5 |
| 4 | 2 | Cardboard Boxes | 50 |
| 5 | 3 | Office Chair | 2 |
| 6 | 4 | Network Switch | 2 |
| 7 | 5 | Screwdriver Set | 5 |
| 8 | 6 | Safety Helmet | 10 |
| 9 | 7 | All-Purpose Cleaner | 15 |
| 10 | 8 | Cardboard Boxes | 100 |
| 11 | 9 | Car Battery | 3 |
| 12 | 10 | First Aid Kit | 5 |

### 7. **Audit Logs** (10 Records)
| ID | Action | Entity | Entity ID | User | Timestamp | Details |
|----|--------|--------|-----------|------|-----------|---------|
| 1 | Created | Category | 1 | admin@stock.com | -35 days | Created Electronics category |
| 2 | Created | Product | 1 | admin@stock.com | -34 days | Added Laptop Dell XPS 13 |
| 3 | Created | GRN | 1 | admin@stock.com | -30 days | Created GRN from TechCorp Inc. |
| 4 | Updated | Product | 1 | admin@stock.com | -29 days | Updated stock quantity for Laptop |
| 5 | Created | GIN | 1 | admin@stock.com | -28 days | Issued laptop to IT Department |
| 6 | Updated | Product | 1 | admin@stock.com | -28 days | Reduced stock after GIN issue |
| 7 | Created | Category | 2 | admin@stock.com | -27 days | Created Office Supplies category |
| 8 | Created | Product | 2 | admin@stock.com | -26 days | Added Wireless Mouse |
| 9 | Updated | GRN | 1 | admin@stock.com | -25 days | Marked GRN as completed |
| 10 | Created | GIN | 2 | admin@stock.com | -23 days | Issued supplies to Marketing Team |

---

## 🚀 How to Apply Test Data

### **Option 1: Reset Database (Recommended)**
```bash
# This will drop and recreate the database with all test data
reset-database.bat
```

### **Option 2: Manual Reset**
```bash
# Stop applications
stop-simple.bat

# Drop and recreate database
cd StockManagementSystem.Api
dotnet ef database drop --force
dotnet ef database update
cd ..

# Start applications
run-simple.bat
```

---

## 📊 Data Relationships

### **Realistic Business Scenarios**
1. **Electronics Purchase**: TechCorp Inc. supplies laptops and mice
2. **Office Setup**: Furniture World provides ergonomic chairs
3. **IT Infrastructure**: Network Pro supplies networking equipment
4. **Safety Compliance**: Safety First Co. provides protective equipment
5. **Maintenance**: ToolMaster Ltd. supplies tools for repairs
6. **Operations**: Various departments receive appropriate supplies

### **Stock Flow**
- **GRNs** increase stock quantities
- **GINs** decrease stock quantities
- **Audit Logs** track all transactions
- **Realistic pricing** and quantities throughout

---

## 🎯 Testing Scenarios

### **1. Stock Management**
- View current stock levels
- Check reorder levels
- Monitor stock movements

### **2. GRN Processing**
- Review received goods
- Verify supplier information
- Check total amounts

### **3. GIN Processing**
- Issue items to departments
- Track reasons for issuance
- Monitor stock depletion

### **4. Reporting**
- Generate stock reports
- View audit trails
- Analyze trends

### **5. Low Stock Alerts**
- Products near reorder levels
- Automatic notifications
- Reorder suggestions

---

## 🔑 Default Login
- **Email**: admin@stock.com
- **Password**: Admin@123
- **Role**: Admin (full access)

---

## 📈 Benefits of Test Data

1. **Immediate Testing**: No need to manually create data
2. **Realistic Scenarios**: Business-like data relationships
3. **Complete Coverage**: All tables populated
4. **Audit Trail**: Full transaction history
5. **Performance Testing**: Sufficient data volume
6. **Demo Ready**: Professional presentation

---

## 🛠️ Customization

To modify the test data:
1. Edit `StockManagementSystem.Api/Models/DbInitializer.cs`
2. Update the `SeedTestDataAsync` method
3. Run `reset-database.bat` to apply changes

---

**Happy Testing! 🎉** 