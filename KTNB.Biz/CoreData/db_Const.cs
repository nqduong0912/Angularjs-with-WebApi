namespace vpb.app.business.ktnb.CoreData
{
    using System;

    internal static class db_Const
    {
        public const byte ADDNEW = 1;
        public const byte ADDNEW_ON_PROCESS = 2;
        public const string AMND_STATE_ACTIVE = "A";
        public const string AMND_STATE_BLOCK = "B";
        public const string AMND_STATE_DELETE = "D";
        public const string AMND_STATE_INACTIVE = "I";
        public const byte EDIT = 5;
        public const byte EDIT_ON_PROCESS = 6;
        public const int OPERATION_TYPE_CREATE = 5;
        public const int OPERATION_TYPE_DELETE = 9;
        public const int OPERATION_TYPE_EDIT = 7;
        public const int OPERATION_TYPE_LOGOUT = 3;
        public const int OPERATION_TYPE_LOGIN = 1;
        public const int OPERATION_TYPE_READ = 11;
        public const byte PERMISSION_CREATE_ABLE = 8;
        public const byte PERMISSION_DELETE_ABLE = 4;
        public const byte PERMISSION_EDIT_ABLE = 2;
        public const byte PERMISSION_FULL = 15;
        public const byte PERMISSION_READ_ABLE = 1;
        public const byte REPORT = 7;
        public const byte SHOW = 3;
        public const byte SHOW_ON_PROCESS = 4;
        public const byte TYPE_OF_APPLICANT_GROUP = 4;
        public const byte TYPE_OF_APPLICANT_ROLE = 2;
        public const byte TYPE_OF_APPLICANT_USER = 1;
        public const byte TYPE_OF_LINK_DOCUMENT = 11;
        public const byte TYPE_OF_LINK_GROUP = 0x11;
        public const byte TYPE_OF_LINK_ROLE = 15;
        public const byte TYPE_OF_LINK_USER = 13;
        public const byte TYPE_OF_OBJECT_APPLICATION = 14;
        public const byte TYPE_OF_OBJECT_COMPONENT = 15;
        public const byte TYPE_OF_OBJECT_DOCSPACE = 13;
        public const byte TYPE_OF_OBJECT_DOCUMENT = 11;
        public const byte TYPE_OF_OBJECT_FOLDER = 12;
        public const string USER_ID_ADMIN = "00000000-0000-0000-0000-000000000001";
        public const string USER_ID_EVERYONE = "00000000-0000-0000-0000-000000000002";
        public const string USER_ID_SYSTEM = "00000000-0000-0000-0000-000000000000";
    }
}

