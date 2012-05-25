﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.SharpDevelop.Dom;

namespace ICSharpCode.PackageManagement.EnvDTE
{
	public class CodeModel : MarshalByRefObject
	{
		IProjectContent projectContent;
		ProjectCodeElements codeElements;
		
		public CodeModel(IProjectContent projectContent)
		{
			this.projectContent = projectContent;
		}
		
		public CodeElements CodeElements {
			get {
				if (codeElements == null) {
					codeElements = new ProjectCodeElements(projectContent);
				}
				return codeElements;
			}
		}
		
		public CodeType CodeTypeFromFullName(string name)
		{
			IClass matchedClass = projectContent.GetClass(name, 0);
			if (matchedClass != null) {
				return CreateCodeTypeForClass(matchedClass);
			}
			return null;
		}
		
		CodeType CreateCodeTypeForClass(IClass @class)
		{
			if (@class.ClassType == ClassType.Interface) {
				return new CodeInterface(@class);
			}
			return new CodeClass2(@class);
		}
	}
}
