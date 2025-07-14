import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-add-term-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './add-term-dialog.html',
  styleUrls: ['./add-term-dialog.css'],
})
export class AddTermDialog {
  term: string = '';
  definition: string = '';

  constructor(private dialogRef: MatDialogRef<AddTermDialog>) {}

  onCancel() {
    this.dialogRef.close();
  }

  onAdd() {
    if (this.term.trim() && this.definition.trim()) {
      this.dialogRef.close({ term: this.term.trim(), definition: this.definition.trim() });
    }
  }
}
