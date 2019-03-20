using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Querymanager insertNewStudent = new Querymanager();
            string studentTable = "CSE142";
            IDictionary<string, object> newStudentList = new Dictionary<string,object>();
            insertNewStudent.Insert(studentTable, newStudentList);
            insertNewStudent.Commit();

            //later decide these students are not part of CSE 142 class
            insertNewStudent.RollBack();
        }
    }


    public enum BinaryOperation
    {
        Equals,
        NotEquals,
        GreaterThan,
        GreaterThanOrEquals,
        LessThan,
        LessThanOrEquals,
        In,
        LIke,
        IsNull
    }

    public interface IPredicate
    {
        string ColumnName { get; }
        BinaryOperation Operation { get; }
        object Value { get; }
    }

    public class Predicate : IPredicate
    {
        public string ColumnName { get; }

        public BinaryOperation Operation { get;}

        public object Value { get; }
    }

    public interface IQueryManager
    {
        void Insert(string tableName, IDictionary<string, object> columnValues);
        void Update(string tableName, IDictionary<string, object> columnValues, IList<IPredicate> predicates);
        void Delete(string tableName, IList<IPredicate> predicates);
        bool Commit();
        bool RollBack();
    }

    public class Querymanager : IQueryManager
    {
        private string insertTable;
        private IDictionary<string, object> insertColumnValues;
        private string updateTable;
        private IDictionary<string, object> updateColumnValues;
        private IList<IPredicate> updatePredicates;
        private string deleteTable;
        private IList<IPredicate> deletePredicates;

        //temp record holder before updates
        private string updateTablePrevious;
        private IDictionary<string, object> updateColumnValuesPrevious;
        private IList<IPredicate> updatePredicatesPrivous;

        //temp record holder before delete
        private string deleteTableHolder;
        private IList<IPredicate> deletePredicatesHolder;

        public bool Commit()
        {
            if (insertTable != string.Empty && insertColumnValues.Count > 0)
            {
                return insertRecordIntoTable();
            }
            else if (updateTable != string.Empty && updateColumnValues.Count > 0 && updatePredicates.Count > 0)
            {
                return updateRecordIntoTable();

            }else if(deleteTable != string.Empty && deletePredicates.Count > 0)
            {
                return deleteRecordIntoTable();
            }
            else
            {
                return false;
            }          
           
        }

        public void Delete(string tableName, IList<IPredicate> predicates)
        {
            deleteTable = tableName;
            deletePredicates = predicates;         
        }

        public void Insert(string tableName, IDictionary<string, object> columnValues)
        {
            insertTable = tableName;
            insertColumnValues = columnValues;
        }

        public bool RollBack()
        {
            if (insertTable != string.Empty && insertColumnValues.Count > 0)
            {
                return deleteRecordIntoTable();
            }
            else if (updateTable != string.Empty && updateColumnValues.Count > 0 && updatePredicates.Count > 0)
            {
                return updateRecordIntoTableRollBack();

            }
            else if (deleteTable != string.Empty && deletePredicates.Count > 0)
            {
                return deleteRecordIntoTableRollBack();
            }
            else
            {
                return false;
            }
        }

        public void Update(string tableName, IDictionary<string, object> columnValues, IList<IPredicate> predicates)
        {
            updateTable = tableName;
            updateColumnValues = columnValues;
            updatePredicates = predicates;
        }

        private bool updateRecordIntoTable()
        {
            //save current record value to (Dictionary) updateColumnValuesPrevious
            //save current predicate to (IList) updatePredicatesPrivous
            //udpate record to table
            //success return true
            //not success return false
            return true;
        }

        private bool updateRecordIntoTableRollBack()
        {
            //udpate records to table from (Dictionary) updateColumnValuesPrevious
            //success return true
            //not success return false
            return true;
        }

        private bool insertRecordIntoTable()
        {
            //insert record to table
            //success return true
            //not success return false
            return true;
        }

        private bool deleteRecordIntoTable()
        {
            //update deleteTableHolder;
            //update (IList) deletePredicatesHolder;
            //delete record to table
            //success return true
            //not success return false
            return true;
        }

        private bool deleteRecordIntoTableRollBack()
        {
            //insert record into table from (IList) deletePredicatesHolder
            //if success return true
            //if not success return false
            return true;
        }
    }
}
