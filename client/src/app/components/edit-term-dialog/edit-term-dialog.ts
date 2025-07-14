import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-edit-term-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './edit-term-dialog.html',
  styleUrls: ['./edit-term-dialog.css'],
})
export class EditTermDialog {
  term: string = '';
  definition: string = '';

  constructor(
    private dialogRef: MatDialogRef<EditTermDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { term: string; definition: string }
  ) {
    this.term = data?.term ?? '';
    this.definition = data?.definition ?? '';
  }

  onCancel() {
    this.dialogRef.close();
  }

  onAdd() {
    if (this.term.trim() && this.definition.trim()) {
      this.dialogRef.close({
        term: this.term.trim(),
        definition: this.definition.trim(),
      });
    }
  }
}

