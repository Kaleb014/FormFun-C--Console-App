module com.example.consoleapp1 {
    requires javafx.controls;
    requires javafx.fxml;


    opens com.example.consoleapp1 to javafx.fxml;
    exports com.example.consoleapp1;
}