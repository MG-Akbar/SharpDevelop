﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using ICSharpCode.PythonBinding;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Resolver
{
	[TestFixture]
	public class ResolveSystemImportedAsMySystemTestFixture : ResolveTestFixtureBase
	{
		protected override ExpressionResult GetExpressionResult()
		{
			List<ICompletionEntry> namespaceItems = new List<ICompletionEntry>();
			DefaultClass consoleClass = new DefaultClass(compilationUnit, "System.Console");
			namespaceItems.Add(consoleClass);
			projectContent.AddExistingNamespaceContents("System", namespaceItems);
			
			return new ExpressionResult("MySystem", ExpressionContext.Default);
		}
		
		protected override string GetPythonScript()
		{
			return 
				"import System as MySystem\r\n" +
				"MySystem.\r\n" +
				"\r\n";
		}
				
		[Test]
		public void ResolveResultContainsConsoleClass()
		{
			List<ICompletionEntry> items = GetCompletionItems();
			IClass consoleClass = PythonCompletionItemsHelper.FindClassFromCollection("Console", items);
			Assert.IsNotNull(consoleClass);
		}
		
		List<ICompletionEntry> GetCompletionItems()
		{
			return resolveResult.GetCompletionData(projectContent);
		}
		
		[Test]
		public void NamespaceResolveResultNameIsSystem()
		{
			NamespaceResolveResult namespaceResolveResult = resolveResult as NamespaceResolveResult;
			Assert.AreEqual("System", namespaceResolveResult.Name);
		}
		
		[Test]
		public void MockProjectContentSystemNamespaceContentsIncludesConsoleClass()
		{
			List<ICompletionEntry> items = projectContent.GetNamespaceContents("System");
			IClass consoleClass = PythonCompletionItemsHelper.FindClassFromCollection("Console", items);
			Assert.IsNotNull(consoleClass);
		}
		
		[Test]
		public void MockProjectContentNamespaceExistsReturnsTrueForSystem()
		{
			Assert.IsTrue(projectContent.NamespaceExists("System"));
		}
	}
}
