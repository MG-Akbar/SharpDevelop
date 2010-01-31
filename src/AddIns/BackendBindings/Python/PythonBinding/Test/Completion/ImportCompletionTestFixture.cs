﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.PythonBinding;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Completion
{
	[TestFixture]
	public class ImportCompletionTestFixture
	{
		List<ICompletionEntry> completionItems;
		MockProjectContent projectContent;
		
		[SetUp]
		public void Init()
		{
			projectContent = new MockProjectContent();
			ParseInformation parseInfo = new ParseInformation(new DefaultCompilationUnit(projectContent));
			List<ICompletionEntry> namespaceItems = new List<ICompletionEntry>();
			namespaceItems.Add(new NamespaceEntry("Test"));
			projectContent.AddExistingNamespaceContents(String.Empty, namespaceItems);
			
			PythonImportCompletion completion = new PythonImportCompletion(projectContent);
			completionItems = completion.GetCompletionItems();
		}
		
		[Test]
		public void TestNamespaceIsAddedToCompletionItems()
		{
			Assert.Contains(new NamespaceEntry("Test"), completionItems);
		}
		
		[Test]
		public void MathStandardPythonModuleIsAddedToCompletionItems()
		{
			Assert.Contains(new NamespaceEntry("math"), completionItems);
		}
		
		[Test]
		public void NamespacePassedToProjectContentGetNamespaceContentsIsEmptyString()
		{
			Assert.AreEqual(String.Empty, projectContent.NamespacePassedToGetNamespaceContentsMethod);
		}
	}
}
